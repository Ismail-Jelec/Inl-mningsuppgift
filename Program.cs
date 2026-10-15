// Regler och krav från uppgiften. [x] = klart på dev, [ ] = inte klart än.

// --- Kortens värde ---
// [x] 2–10 har sitt eget värde (Hand)
// [x] Knekt, dam och kung är värda 10 (Hand)
// [x] Ess räknas som 1 eller 11, det som är bäst utan att spräcka (Hand)
// [x] Mjuk/hård hand räknas om varje gång, esset kan byta värde under handen (Hand)

// --- Given ---
// [ ] Varje spelare får två kort, öppet (Round)
// [ ] Givaren får ett öppet kort och ett dolt hålkort (Dealer klar, utdelningen i Round saknas)
// [x] Naturlig tjugoett = 21 på de två första korten (Hand.IsNatural)

// --- Spelarnas turer ---
// [ ] Spelarna spelar en i taget, i tur och ordning (Round)
// [ ] Spelaren väljer upprepade gånger mellan köpa och stanna (Move, Player, ConsoleUi)
// [x] Över 21 är spräckt (Hand.IsBust)
// [ ] Spräckt hand förloras direkt, oavsett vad givaren gör (OutcomeResolver)

// --- Givarens tur ---
// [ ] Givaren spelar först när alla spelare är klara (Round)
// [x] Givaren vänder hålkortet (Dealer.RevealHoleCard)
// [x] Givaren köper på 16 eller lägre (Dealer.ShouldHit)
// [x] Givaren stannar på 17 eller högre, även mjuk 17 (Dealer.ShouldHit)
// [ ] Givaren tittar inte på hålkortet i förväg, rundan spelas alltid färdigt (Round)

// --- Avgörande ---
// [ ] Spelaren har spräckt: förlust, även om givaren också spräcker
// [ ] Givaren har spräckt, spelaren inte: vinst
// [ ] Ingen har spräckt: högst hand vinner
// [ ] Lika värde: lika, varken vinst eller förlust
// [ ] Naturlig tjugoett slår 21 på tre eller fler kort
// [ ] Naturlig tjugoett på båda sidor: lika

// --- Kortleken ---
// [x] En lek med 52 kort (Deck)
// [x] Blandas med Fisher–Yates (Deck.Shuffle)
// [ ] En Random skapas i Program och delas av hela programmet (Deck tar emot den, Program saknas)
// [ ] Under 15 kort kvar: ny blandad lek inför nästa runda (Deck.NeedsReshuffle finns, anropet i Round saknas)

// --- Grundversionen ---
// [ ] Inga marker, bara vunna, förlorade och lika rundor per spelare (Player, GameStorage)

// --- Struktur ---
// [x] Ingen Console i regelklasserna, all in- och utmatning i ConsoleUi (stämmer hittills)

// --- Krav på den gemensamma delen ---
// [ ] En till fem spelare kan läggas till och spela
// [ ] Korten delas ut enligt reglerna och givaren följer sin regel
// [x] Mjuka och hårda händer räknas rätt, inklusive flera ess i samma hand
// [ ] Naturlig tjugoett hanteras enligt tabellen
// [ ] Felaktig inmatning hanteras utan att programmet kraschar
// [ ] Spelare och resultat sparas till fil och läses tillbaka vid nästa start
// [ ] Många rundor i följd går att spela utan att kortleken tar slut

Console.WriteLine("Hello, World!");
