import psycopg2, urllib.request, json


def main():

    #letölti a json, deszerializálja és elmenti a data nevű változóba
    with urllib.request.urlopen("https://restcountries.eu/rest/v2/all") as url:
        data = json.loads(url.read().decode())

    #Ezeket az adatokat kell megváltoztatni egy bizonyos adatbázis használatához
    conn = psycopg2.connect(
        host = "localhost",
        database = "orszagok",
        user = "postgres",
        password = "dsaasd"
    )

    #csinál egy cursort az adott connectionnel
    cur = conn.cursor()

    #ha már van countries akkor droppolja
    cur.execute("DROP TABLE IF EXISTS Countries")

    #létrehozza a countries táblát
    cur.execute("""
    CREATE TABLE Countries(
        ID SERIAL PRIMARY KEY,
        Name TEXT,
        Capital TEXT,
        Region TEXT,
        Population int8,
        TopLevelDomain TEXT,
        Area float4,
        Subregion TEXT,
        NativeName TEXT)
    """)

    #feltölti a countries táblát a datában lévő adatok segítségével
    for country in data:
        cur.execute("INSERT INTO Countries(Name, Capital, Region, Population, TopLevelDomain, Area, Subregion, NativeName) VALUES (%s, %s, %s, %s, %s, %s, %s, %s)", (country['name'], country['capital'], country['region'], country['population'], country['topLevelDomain'], country['area'], country['subregion'], country['nativeName']))

    #lekéri a sorok számát, majd ezt kiírja
    cur.execute("SELECT COUNT(*)FROM countries")
    rows = cur.fetchall()
    print(rows)

    #commitolja a változtatásokat és lezárja a kapcsolatokat
    conn.commit()
    cur.close()
    conn.close()

main()