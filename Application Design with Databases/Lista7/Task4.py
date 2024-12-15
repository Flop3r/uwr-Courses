from neo4j import GraphDatabase
import pandas as pd

uri = "neo4j+s://bf648949.databases.neo4j.io"
username = "neo4j" 
password = "password"

driver = GraphDatabase.driver(uri, auth=(username, password))

def fetch_data(query):
    with driver.session() as session:
        fetched_data = session.run(query)
        result = []
        for record in fetched_data:
            result.append(record.data())
    
    return result

def print_results(results):
    if results:
        df = pd.DataFrame(results)
        print(df)
    else:
        print("No data found.")


query = "MATCH (p:Person) RETURN p.name AS name"
persons = fetch_data(query)

print_results(persons)
