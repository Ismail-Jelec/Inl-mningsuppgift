# Tjugoett

Grupp 7. Konsolspel i C# där 1–5 spelare spelar tjugoett mot en givare.

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
- `SimpleStrategy` – en datorspelare som köper tills den har 17 eller mer. Den bryr sig inte om vad givaren har.
- `BasicStrategy` – en datorspelare som följer grundstrategin. Den kollar sin hand, om den är mjuk eller hård, och vilket kort givaren visar, och läser sedan av vad den ska göra i en tabell.
- `Game` – menyn och spelloopen. Jag lade till val 5 så att man kan sätta en datorspelare vid bordet och välja vilken strategi den ska ha.

## Klassdiagram

_Läggs in som bild i repot och länkas här._

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
