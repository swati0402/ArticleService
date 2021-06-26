# ArticleService
Publisher and Consumer Micro service

This  solution have two ASP.NET Core 5 Web API projects “ArticleService” and “ArticleConsumerService”. And One Classlibrary project "ArticleService.core".


Disable HTTPS and activate OpenAPI Support.
For both projects install the following NuGet packages:
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Sqlite
Newtonsoft.Json
RabbitMQ.Client

Mesage Exchange:

I have used RabbitMq for exchanges messages
The easiest way to get RabbitMQ running is to install Docker Desktop. Then issue the following command (in one line in a console window) to start a RabbitMQ container with admin UI :
C:\dev>docker run -d  -p 15672:15672 -p 5672:5672 --hostname my-rabbit --name some-rabbit rabbitmq:3-management

Open your browser on port 15672 and log in with the username “guest” and the password “guest”. 
Use the web UI to create an Exchange with the name “article” of type “Fanout” and two queues “article.articleservice” and “article.articleservice”.

To Test:

Change the App-URL of the ArtcleConsumerService to another port (e.g. http://localhost:5001)
so that both projects can be run in parallel. Configure the solution to start both projects and start debugging.

From PostMan run below to test:

1. To test published message:
Request url: http://localhost:5000/article
Request Type: Post
Request Body:
{
"AssetId": "0f9b15f5-f048-d9f8-b9f1-e201f110b22f" ,
"Slug": "retail-clinics-next-frontier-of-primary-care-delivery-patients-receptive",
"Thumbnail": "https://s3.amazonaws.com/AKIAJC5RLADLUMVRPFDQ.book-thumb-images/herrington.jpg",
"Url": "https://en.wikipedia.org/wiki/Test_article",
"PublicationDateUtc": "2020-12-03T05:00:00Z",
"Author": "Test Author",
"AssetType": "Article",
"Body": "html-of-article",
"Geographies": ["United States"],
"Topics": ["Digital Health"],
"Version": 1
}

2. To test withdraw artticle
Request url : http://localhost:5000/article?assetid=0f9b15f5-f048-d9f8-b9f1-e201f110b22f&version=7

Request type: Delete

PS: Postman Sample are also added in git
"# ContentService" 
