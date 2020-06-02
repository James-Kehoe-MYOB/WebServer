using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

namespace FrameworklessWebServer.DataAccess {
    public class DynamoHandler {
        private static AmazonDynamoDBClient client;
        private static bool operationFailed;
        private static bool operationSucceeded;
        public static bool createClient( bool useDynamoDBLocal )
        {
            if( useDynamoDBLocal )
            {
                operationSucceeded = false;
                operationFailed = false;

                // First, check to see whether anyone is listening on the DynamoDB local port
                // (by default, this is port 8000, so if you are using a different port, modify this accordingly)
                bool localFound = false;
                try
                {
                    using (var tcp_client = new TcpClient())
                    {
                        var result = tcp_client.BeginConnect("localhost", 8000, null, null);
                        localFound = result.AsyncWaitHandle.WaitOne(3000); // Wait 3 seconds
                        tcp_client.EndConnect(result);
                    }
                }
                catch
                {
                    localFound =  false;
                }
                if( !localFound )
                {
                    Console.WriteLine("\n      ERROR: DynamoDB Local does not appear to have been started..." +
                                      "\n        (checked port 8000)");
                    operationFailed = true;
                    return (false);
                }

                // If DynamoDB-Local does seem to be running, so create a client
                Console.WriteLine( "  -- Setting up a DynamoDB-Local client (DynamoDB Local seems to be running)" );
                AmazonDynamoDBConfig ddbConfig = new AmazonDynamoDBConfig();
                ddbConfig.ServiceURL = "http://localhost:8000";
                try { client = new AmazonDynamoDBClient( ddbConfig ); }
                catch( Exception ex )
                {
                    Console.WriteLine( "     FAILED to create a DynamoDBLocal client; " + ex.Message );
                    operationFailed = true;
                    return false;
                }
            }
            else
            {
                try { client = new AmazonDynamoDBClient( ); }
                catch( Exception ex )
                {
                    Console.WriteLine( "     FAILED to create a DynamoDB client; " + ex.Message );
                    operationFailed = true;
                }
            }
            operationSucceeded = true;
            return true;
        }

        public async Task Update(Student person) {
            var clientCreated = createClient(false);
            if (clientCreated) {
                
                var studentJson = JsonConvert.SerializeObject(person);
                
                var studentTable = Table.LoadTable(client, "Students");
                await studentTable.PutItemAsync(Document.FromJson(studentJson));
                
            }
            else {
                throw new Exception();
            }
        }

        public async Task<Student> GetStudentByID(string id) {
            var clientCreated = createClient(false);
            
            if (!clientCreated) throw new Exception();
            
            var hash = new Primitive(id, false);
            var studentTable = Table.LoadTable(client, "Students");
            var studentData = await studentTable.GetItemAsync(hash);
            if (studentData != null) {
                var studentJson = studentData.ToJsonPretty();
                return JsonConvert.DeserializeObject<Student>(studentJson);
            }
            else {
                return null;
            }
        }

        public async Task<IEnumerable<Student>> GetStudents() {
            var clientCreated = createClient(false);
            if (!clientCreated) throw new Exception();

            var studentTable = Table.LoadTable(client, "Students");
            
            var search = studentTable.Scan(new ScanFilter());
            var studentDocuments = new List<Document>();
            do {
                studentDocuments.AddRange(await search.GetNextSetAsync());
            } while (!search.IsDone);

            return (from student in studentDocuments where student != null select student.ToJson() 
                into studentJson select JsonConvert.DeserializeObject<Student>(studentJson)).ToList();

        }

        public async Task DeleteStudentByID(string id) {
            var clientCreated = createClient(false);
            if (!clientCreated) throw new Exception();
            
            var hash = new Primitive(id, false);
            var studentTable = Table.LoadTable(client, "Students");
            var delItem = studentTable.DeleteItemAsync( hash );
            await delItem;
        }
    }
}