# Tjugoett

Grupp 7. Konsolspel i C# där 1-5 spelare spelar tjugoett mot en givare.

## Starta programmet

Kräver .NET 10.

```
dotnet run
```

## Arbetssätt

- `dev` är vår main. All kod kommer in via PR med 1 godkännande.
- En branch per kort, t.ex. `feature-hand-value`, skapad från `dev`.
- En klass per fil.

## Klassöversikt

_Fylls i när grunden är klar._

## Klassdiagram

- `Program` - skapar den enda `Random` i programmet, läser in husreglerna och startar `Game`.
- `Game` - menyn och spelloopen. Lägger till och tar bort spelare, startar en `Round` per giv och sparar spelarna efter varje runda.
- `Round` - en giv: insatser, utdelning, spelarnas turer, givarens tur, avgörande och utbetalning.
- `Suit`, `Rank` - enum:ar för färg och valör.
- `Card` - ett kort med färg och valör. Har inget värde, eftersom essets värde beror på resten av handen.
- `Hand` - korten en deltagare har. Räknar ut värdet varje gång det efterfrågas och vet om handen är mjuk, spräckt eller naturlig tjugoett.
- `Shoe` - skon med flera lekar. Blandar med Fisher-Yates, delar ut kort och har ett skiljekort i den bakre fjärdedelen som avgör när skon blandas om.
- `Participant` - basklass för `Player` och `Dealer`: ett namn, en hand och att ta emot kort.
- `Player` - ärver `Participant`. Har vinster, förluster, lika och marker, och frågar sin strategi om drag och insats.
- `Dealer` - ärver `Participant`. Har hålkortet och den fasta regeln för när givaren köper.
- `Move` - enum för spelarens drag: köpa eller stanna.
- `Outcome` - enum för utfallet: vinst, förlust, lika eller naturlig tjugoett.
- `OutcomeResolver` - jämför en spelares hand med givarens och avgör utfallet.
- `IPlayerStrategy` - gränssnitt för hur en spelare väljer drag och insats. `Player` frågar strategin i stället för konsolen.
- `HumanStrategy` - frågar användaren via `ConsoleUi`.
- `SimpleStrategy` - datorstrategi som köper så länge handen är under 17.
- `BasicStrategy` - datorstrategi som följer grundstrategin genom att slå upp draget i en tabell utifrån handens värde, om den är mjuk eller hård, och givarens öppna kort.
- `Bankroll` - spelarens marker och insats. Kontrollerar att insatsen är giltig och betalar ut efter utfallet.
- `RuleSet` - husreglerna: om givaren köper på mjuk 17, antal lekar, när det blandas om och om naturlig tjugoett slår 21 på tre kort.
- `RuleSetLoader` - läser in `RuleSet` från `rules.txt`. Saknas filen eller är en rad fel används standardvärden och en varning visas.
- `CardCounter` - korträknare med Hi-Lo. Observerar korten efter varje runda och ger löpande och sann räkning. Ändrar ingenting i spelet.
- `CountTrend` - enum för hur gynnsam räkningen är: ogynnsamt, neutralt eller gynnsamt.
- `ConsoleUi` - all in- och utmatning. Visar händer och resultat, frågar och validerar svar.
- `CardRenderer` - ritar ett kort som rader text (`string[]`), så att `ConsoleUi` kan skriva ut flera kort bredvid varandra.
- `GameStorage` - sparar och läser spelarna i `players.json`.
- `PlayerRecord` - det som sparas om en spelare mellan körningar: namn, vinster, förluster, lika och marker.

Ingen regelklass skriver ut eller läser in något. All kontakt med användaren går genom `ConsoleUi`.

## Vem gjorde vad

|  Namn  | Tillval | Del av grunden |
| ------ | ------- | -------------- |
| Ismail | 1, 5    |                |
| Albin  | 2       |                |
| Ayaan  |         |                |
| Adrian |         |                |


## Avvikelser från reglerna

_Inga än._

## Kända brister

_Inga än._
