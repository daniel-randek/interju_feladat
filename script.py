import psycopg2, urllib.request, json

with urllib.request.urlopen("https://restcountries.eu/rest/v2/all") as url:
    data = json.loads(url.read().decode())

conn = psycopg2.connect(
    host = "localhost",
    database = "orszagok",
    user = "postgres",
    password = "dsaasd"
)

cur = conn.cursor()

cur.execute("DROP TABLE IF EXISTS Countries")
cur.execute("DROP TABLE IF EXISTS Countries123")

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

for country in data:
    cur.execute("INSERT INTO Countries(Name, Capital, Region, Population, TopLevelDomain, Area, Subregion, NativeName) VALUES (%s, %s, %s, %s, %s, %s, %s, %s)", (country['name'], country['capital'], country['region'], country['population'], country['topLevelDomain'], country['area'], country['subregion'], country['nativeName']))

cur.execute("SELECT COUNT(*)FROM countries")
rows = cur.fetchall()
print(rows)

conn.commit()
cur.close()
conn.close()