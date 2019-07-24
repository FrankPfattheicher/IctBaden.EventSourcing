# IctBaden.EventSourcing
An EventSourcing Library.

Design goals:
* Minimal impact on application design
* Find a naming that can be understood by any user
* Address all CQRS/EventSourcing [problems](Problems.md)

Currently most projects are not existing or an empty frame only - working on samples.

This implementation is inspired by several publications and frameworks - see [Links](Links.md)


# Samples
To "Eat My Own Dog Food" i created the following samles.

## TicTacToe.Wpf
A classic implementation of the game without any event sourcing.
This is just to have anything to compare the attempts.

## TicTacToe.EventSourcing.Wpf
Implementation using event sourcing for game logic.

# Unit Tests 
## IctBaden.EventSourcing.Tests


# Persistance
Event persistance will be included in the package.

* In memory (mainly for tests)
* [EventStore](eventstore.org)
* Mongodb

