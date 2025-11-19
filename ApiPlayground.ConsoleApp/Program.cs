// See https://aka.ms/new-console-template for more information

using ApiPlayground.ConsoleApp;
using ApiPlayground.Shared;

Console.WriteLine("Hello, World!");

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
// adoDotNetExample.Read();
// adoDotNetExample.Edit(4);
/*adoDotNetExample.Create(
    "God of War",
    "Santa Monica Studio",
    "God of War is a 2018 action-adventure game developed by Santa Monica Studio and published by Sony Interactive Entertainment. The game was released worldwide for the PlayStation 4 in April 2018, with a Windows port released in January 2022.");*/
/*adoDotNetExample.Update(
    1004,
    "Delicious in Dungeon",
    "Ryoko Kui",
    "Delicious in Dungeon, is a Japanese manga series written and illustrated by Ryoko Kui. It was serialized in Enterbrain's seinen manga magazine Harta from February 2014 to September 2023, with its chapters collected in 14 tankōbon volumes.");*/
// adoDotNetExample.Delete(1003);

DapperExample dapperExample = new DapperExample();
// dapperExample.Read();
dapperExample.Edit(7);
/*dapperExample.Create(
    "God Hand",
    "Clover Studio",
    "God Hand is a 2006 beat 'em up game developed by Clover Studio and published by Capcom for the PlayStation 2. It was released in Japan and North America in 2006, and in 2007 for PAL territories. It was re-released for the PlayStation 3 as a PS2 Classics downloadable game on the PlayStation Network on October 4, 2011.");*/
/*dapperExample.Update(
    1002,
    "Sengoku Basara",
    "Capcom",
    "Sengoku BASARA 4 is the fourth main installment of the Sengoku BASARA video game series, developed and published by Capcom for the PlayStation 3. The game was released in Japan on January 23, 2014.");*/
// dapperExample.Delete(7);