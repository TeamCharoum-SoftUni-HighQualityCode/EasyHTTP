using Machine.Specifications;

namespace EasyHttp.Specs.Helpers
{
    public class DataSpecificationBase : IAssemblyContext
    {
        private ServiceStackHost appHost;
        private int port;

        void IAssemblyContext.OnAssemblyComplete()
        {
            this.appHost.Dispose();    
        }

        void IAssemblyContext.OnAssemblyStart()
        {
            this.port = 16000;
            var listeningOn = "http://localhost:" + this.port + "/";
            this.appHost = new ServiceStackHost();
            this.appHost.Init();
            this.appHost.Start(listeningOn); 
        }

    }
}