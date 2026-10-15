// Regler och krav från uppgiften, ordnade efter fil. [x] = klart på dev, [ ] = inte klart än.
// Varje stycke har en ansvarig. Rör bara dina egna filer, och bocka bara av i ditt eget stycke.

// === Hand.cs === Klar
// [x] 2–10 har sitt eget värde
// [x] Knekt, dam och kung är värda 10
// [x] Ess räknas som 1 eller 11, det som är bäst utan att spräcka
// [x] Mjuk/hård hand räknas om varje gång, esset kan byta värde under handen
// [x] Mjuka och hårda händer räknas rätt, inklusive flera ess i samma hand
// [x] Naturlig tjugoett = 21 på de två första korten
// [x] Över 21 är spräckt

// === Dealer.cs === Klar
// [x] Givaren vänder hålkortet
// [x] Givaren köper på 16 eller lägre
// [x] Givaren stannar på 17 eller högre, även mjuk 17

// === Deck.cs === Klar
// [x] En lek med 52 kort
// [x] Blandas med Fisher–Yates

// === Move.cs, Outcome.cs, OutcomeResolver.cs === Ansvarig:
// [ ] Spelaren har spräckt: förlust, även om givaren också spräcker
// [ ] Givaren har spräckt, spelaren inte: vinst
// [ ] Ingen har spräckt: högst hand vinner
// [ ] Lika värde: lika, varken vinst eller förlust
// [ ] Naturlig tjugoett slår 21 på tre eller fler kort
// [ ] Naturlig tjugoett på båda sidor: lika
// [ ] Naturlig tjugoett hanteras enligt tabellen

// === IPlayerStrategy.cs, Player.cs, HumanStrategy.cs === Ansvarig:
// [ ] Beslutet kommer från en strategi, Player frågar aldrig Console själv
// [ ] Spelaren väljer upprepade gånger mellan köpa och stanna
// [ ] Inga marker, bara vunna, förlorade och lika rundor per spelare

// === Round.cs === Ansvarig:
// [ ] Varje spelare får två kort, öppet
// [ ] Givaren får ett öppet kort och ett dolt hålkort
// [ ] Spelarna spelar en i taget, i tur och ordning
// [ ] Spräckt hand: spelarens tur slutar direkt
// [ ] Givaren spelar först när alla spelare är klara
// [ ] Givaren tittar inte på hålkortet i förväg, rundan spelas alltid färdigt
// [ ] Korten delas ut enligt reglerna och givaren följer sin regel
// [ ] Leken tar aldrig slut mitt i en runda

// === ConsoleUi.cs === Ansvarig:
// [ ] All in- och utmatning sker här, ingen Console i regelklasserna
// [ ] Felaktig inmatning hanteras utan att programmet kraschar

// === GameStorage.cs, Game.cs, Program.cs === Ansvarig: Ismail
// [x] En till fem spelare kan läggas till och spela
// [x] Spelare och resultat sparas till fil och läses tillbaka vid nästa start
// [x] Under 15 kort kvar: ny blandad lek inför nästa runda
// [x] Många rundor i följd går att spela utan att kortleken tar slut
// [x] En Random skapas i Program och delas av hela programmet


var random = new Random(); // den enda Random i hela programmet
var game = new Game(new ConsoleUi(), new GameStorage("players.json"), new Deck(random));
game.Run();