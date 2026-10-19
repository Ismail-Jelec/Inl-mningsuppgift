var random = new Random(); // den enda Random i hela programmet
var game = new Game(new ConsoleUi(),new GameStorage(), new Deck(random));
game.Run();