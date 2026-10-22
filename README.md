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
- `Program` - skapar den enda `Random` i programmet, läser in husreglerna och startar `Game`.
- `Game` - menyn och spelloopen. Lägger till och tar bort spelare, startar en `Round` per giv och sparar spelarna efter varje runda. Val 5 sätter en datorspelare vid bordet och låter en välja vilken strategi den ska ha.
- `Hand` - korten en deltagare har. Räknar ut värdet varje gång det efterfrågas och vet om handen är mjuk, spräckt eller naturlig tjugoett.
- `Dealer` - ärver `Participant`. Har hålkortet och den fasta regeln för när givaren köper.
- `ConsoleUi` - all in- och utmatning. Visar händer och resultat, frågar och validerar svar.
- `CardRenderer` - ritar ett kort som rader text (`string[]`), så att `ConsoleUi` kan skriva ut flera kort bredvid varandra.
- `GameStorage` - sparar och läser spelarna i `players.json`.
- `PlayerRecord` - det som sparas om en spelare mellan körningar: namn, vinster, förluster, lika och marker.
- `SimpleStrategy` – en datorspelare som köper tills den har 17 eller mer. Den bryr sig inte om vad givaren har.
- `BasicStrategy` – en datorspelare som följer grundstrategin. Den kollar sin hand, om den är mjuk eller hård, och vilket kort givaren visar, och läser sedan av vad den ska göra i en tabell.
- `Game` – menyn och spelloopen. Jag lade till val 5 så att man kan sätta en datorspelare vid bordet och välja vilken strategi den ska ha. 
- `Move` – de två drag en spelare kan göra: köpa eller stanna. (små ändringar)
- `Outcome` – hur en runda slutade för en spelare: vinst, förlust, lika eller naturlig tjugoett.
- `OutcomeResolver` – jämför spelarens hand med givarens och avgör vem som vann. Följer tabellen i reglerna, så att den som spräcker alltid förlorar och naturlig tjugoett slår 21 på tre kort.
- `Bankroll` – håller koll på en spelares marker och insats. Den drar insatsen när man satsar och betalar ut efter rundan: vinst 1:1, naturlig tjugoett 3:2 och lika ger insatsen tillbaka.
- `Round` – jag lade till att alla spelare satsar innan korten delas ut, och att insatserna betalas ut när rundan är avgjord. (ändringar)
- `Game` – jag lade till att en spelare som får slut på marker lämnar bordet och inte kan sätta sig igen, och att markerna sparas tillsammans med resten. (ändringar)

## Klassdiagram



## Vem gjorde vad

|  Namn  | Tillval | Del av grunden |
| ------ | ------- | -------------- |
| Ismail | 1       | `Card`, `Deck`, `Hand`, `Dealer` `ConsoleUi`, `Game`, `GameStorage`, `PlayerRecord`, `Program`|
| Albin  | 2       | `Move`, `IPlayerStrategy`, `Player`, `Game` |
| Ayaan  | 4       | `Move`, `Outcome`, `OutcomeResolver` |
| Adrian |         |                |


## Avvikelser från reglerna

_Inga än._

## Kända brister

_Inga än._
