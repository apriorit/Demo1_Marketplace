Detail documentation:
 - Docs\Demo Project.pdf

Before start:
 - Install Elastic + Kibana + Filebeat + Logstash
   - Setup elastic cluster, logging via Filebea: Docs\LoggingViaFilebeat+Elasticsearch.docx
   - Setup synchronizing with DB:  Docs\ElasticsearchRDB_sync.docx
 - Enable https endpoint: https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-tutorial-dotnet-app-enable-https-endpoint.
 - Add a self-signed certificate to make Identity server work. From PowerShell, run command 
C:\program files\microsoft sdks\service fabric\clustersetup\secure> .\CertSetup.ps1 -Install -CertSubjectName CN=mytestcert


To start the project:
 - Open the solution in Visual Studio 2022
 - Make sure "Multiple startup projects" option is selected in solution's Properties: Marketplace.Api and Marketplace.Auth projects should be started
 - Change environment-specific parameters in appsettings.json files for Marketplace.Api and Marketplace.Auth projects


Technologies used:
.NET Core 		6
Entity Framework Core 	6.0.8
Identity Server		4.1.2
ElasticSearch client	8.0.0-beta