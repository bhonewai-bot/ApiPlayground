// See https://aka.ms/new-console-template for more information

using ApiPlayground.ConsoleApp2;

HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.Read();
// await httpClientExample.Edit(102);
// await httpClientExample.Create(2, "Title 1", "Body 1");
// await httpClientExample.Update(1, "Title 1", "Body 1", 10);
// await httpClientExample.Delete(2);

RestClientExample restClientExample = new RestClientExample();
// await restClientExample.Read();