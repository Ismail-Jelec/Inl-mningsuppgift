var random = new Random(); // den enda Random i hela programmet
var ui = new ConsoleUi();

// Reglerna läses in en gång och skickas sedan vidare dit de behövs.
var loader = new RuleSetLoader();
RuleSet rules = loader.Load();
foreach (string warning in loader.Warnings)
{
    ui.ShowMessage(warning);
}

var game = new Game(ui, new GameStorage(), new Shoe(random, rules), rules);
game.Run();