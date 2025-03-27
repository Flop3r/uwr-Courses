---
title: Lista 7

---

# Lista 7
## Task 1
```
CREATE (charlie:Person:Actor {name: 'Charlie Sheen'}),
       (martin:Person:Actor {name: 'Martin Sheen'}),
       (michael:Person:Actor {name: 'Michael Douglas'}),
       (oliver:Person:Director {name: 'Oliver Stone'}),
       (rob:Person:Director {name: 'Rob Reiner'}),
       (wallStreet:Movie {title: 'Wall Street'}),
       (charlie)-[:ACTED_IN {role: 'Bud Fox'}]->(wallStreet),
       (martin)-[:ACTED_IN {role: 'Carl Fox'}]->(wallStreet),
       (michael)-[:ACTED_IN {role: 'Gordon Gekko'}]->(wallStreet),
       (oliver)-[:DIRECTED]->(wallStreet),
       (thePresident:Movie {title: 'The American President'}),
       (martin)-[:ACTED_IN {role: 'A.J. MacInerney'}]->(thePresident),
       (michael)-[:ACTED_IN {role: 'President Andrew Shepherd'}]->(thePresident),
       (rob)-[:DIRECTED]->(thePresident)
```
## Task 2
```
MATCH (n)
DETACH DELETE n;

// Add two new actors
CREATE (:Actor {name: "ActorA"});
CREATE (:Actor {name: "ActorB"});

// Add two new movies
CREATE (:Movie {title: "FilmA"});
CREATE (:Movie {title: "FilmB"});

// Add two new properties to a movie
MATCH (m:Movie {title: "FilmA"})
SET m.genre = "Comedy", m.releaseYear = 2023;

// Add new acted_in relations to the existing nodes
MATCH (a:Actor {name: "ActorA"}), (m:Movie {title: "FilmA"})
CREATE (a)-[:ACTED_IN]->(m);

MATCH (a:Actor {name: "ActorB"}), (m:Movie {title: "FilmB"})
CREATE (a)-[:ACTED_IN]->(m);

// Update a movie property
MATCH (m:Movie {title: "FilmA"})
SET m.genre = "Drama";

// Remove an acted_in relationship
MATCH (a:Actor {name: "ActorA"})-[r:ACTED_IN]->(m:Movie {title: "FilmA"})
DELETE r;
```

## Task 3
```
MATCH (n)
DETACH DELETE n;

CREATE (p1:Actor {name: "ActorA"}), 
       (p2:Actor {name: "ActorB"}), 
       (p3:Actor {name: "ActorC"}), 
       (p4:Actor {name: "ActorD"}), 
       (d1:Director {name: "DirectorA"}), 
       (d2:Director {name: "DirectorB"}), 
       (m1:Movie {title: "MovieA"}), 
       (m2:Movie {title: "MovieB"}), 
       (m3:Movie {title: "MovieC"}), 
       
       (p1)-[:ACTED_IN]->(m1), 
       
       (p2)-[:ACTED_IN]->(m1), 
       (p2)-[:ACTED_IN]->(m2), 
       (p2)-[:ACTED_IN]->(m3), 
       (p2)-[:DIRECTED]->(m1),
       
       (p3)-[:ACTED_IN]->(m3),
       
       (d1)-[:DIRECTED]->(m1), 
       (d2)-[:DIRECTED]->(m2), 
       (d2)-[:DIRECTED]->(m3);


// 1. Return the movies where person A acted in
MATCH (actor:Actor {name: "ActorA"})-[:ACTED_IN]->(movie:Movie)
RETURN movie;

// 2. Return the movies where person B was both the actor and the director
MATCH (actor:Actor {name: "ActorB"})-[:ACTED_IN]->(movie:Movie)<-[:DIRECTED]-(actor)
RETURN movie;

// 3. Return actors who didn’t play in any movie
MATCH (actor:Actor)
WHERE NOT (actor)-[:ACTED_IN]->(:Movie)
RETURN actor;

// 4. Return actors who played in more than 2 movies
MATCH (actor:Actor)-[:ACTED_IN]->(movie:Movie)
WITH actor, count(movie) AS moviesCount
WHERE moviesCount > 2
RETURN actor;

// 5. Return movies with the larger number of actors
MATCH (movie:Movie)<-[:ACTED_IN]-(actor:Actor)
WITH movie, count(actor) AS actorCount
ORDER BY actorCount DESC
RETURN movie;
```

## Task 4
```
from neo4j import GraphDatabase
import pandas as pd

uri = "neo4j+s://bf648949.databases.neo4j.io"
username = "neo4j" 
password = "password"

driver = GraphDatabase.driver(uri, auth=(username, password))

def fetch_persons():
    query = "MATCH (p:Person) RETURN p.name AS name"
    
    with driver.session() as session:
        result = session.run(query)
        persons = []
        for record in result:
            persons.append(record.data())
    
    return persons

def print_persons_table(persons):
    if persons:
        df = pd.DataFrame(persons)
        print(df)
    else:
        print("No persons found.")

if __name__ == "__main__":
    persons = fetch_persons()
    print_persons_table(persons)

```