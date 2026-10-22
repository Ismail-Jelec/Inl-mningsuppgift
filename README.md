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

- `Program` - skapar den enda `Random` i programmet, läser in husreglerna och startar 
- `Game` - menyn och spelloopen. Lägger till och tar bort spelare, startar en `Round` per giv och sparar spelarna efter varje runda.

- `Hand` - korten en deltagare har. Räknar ut värdet varje gång det efterfrågas och vet om handen är mjuk, spräckt eller naturlig tjugoett.
s.
- `Dealer` - ärver `Participant`. Har hålkortet och den fasta regeln för när givaren köper.
- `ConsoleUi` - all in- och utmatning. Visar händer och resultat, frågar och validerar svar.
- `CardRenderer` - ritar ett kort som rader text (`string[]`), så att `ConsoleUi` kan skriva ut flera kort bredvid varandra.
- `GameStorage` - sparar och läser spelarna i `players.json`.
- `PlayerRecord` - det som sparas om en spelare mellan körningar: namn, vinster, förluster, lika och marker.


## Vem gjorde vad

|  Namn  | Tillval | Del av grunden |
| ------ | ------- | -------------- |
| Ismail | 1       | `Card`, `Suit`, `Rank`, `Deck`, `Hand`, `Participant`, `Dealer` `ConsoleUi`, `Game`, `GameStorage`, `PlayerRecord`, `Program`, `Player` |
| Albin  | 2       |                |
| Ayaan  |         |                |
| Adrian |         |                |


## Avvikelser från reglerna

_Inga än._

## Kända brister

_Inga än._
