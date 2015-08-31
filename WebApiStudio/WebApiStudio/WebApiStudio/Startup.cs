using System.ComponentModel.Composition;
using System.Web.Http;
using Eze.WebApiEx;
using Owin;

namespace WebApiStudio
{
    public class Startup
    {
        [Export( "apiPath" )]
        public const string ApiPath = "api/v1";

        [Export( "DefaultApiRouteName" )]
        public const string DefaultApiRouteName = "api";

        [Export( "dbName" )]
        public const string DbName = "demo";

        //The name of the XML file visual studio generates from /// comments;
        //Make sure this path matches the value in Project Properties -> Build -> "XML documentation file"
        [Export( "XmlDocPath" )]
        public const string XmlDocPath = "docs.xml";

        public HttpConfiguration Config { get; private set; }

        public void Configuration( IAppBuilder app )
        {
            Config = new HttpConfiguration();

            Config.UseSwaggerDocumentation( ApiPath, XmlDocPath );
            Config.UseResourceControllers();
            Config.UseBasicDefaults( DefaultApiRouteName, ApiPath );

            app.UseWebApi( Config );
        }
    }
}
