namespace FrameworklessWebServer {
    public interface HTTPMethods {
        void Get();

        void Post();

        void Put();

        void Patch();

        void Delete();
    }
}