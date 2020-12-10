import requests
import json


def get():
    url = "..."
    ret = requests.get(url)
    print(ret.status_code)
    print(ret.text)


def post():
    payload = { 'getal1': 4, 'getal2': 2 }
    url = "..."
    json = json.dumps(payload)
    ret = requests.post(url, data=json)
    print(ret.status_code)
    print(ret.text)

    # Load variable form JSON
    jsonstring = ret.text
    obj = json.loads(jsonstring)
    print(str(obj["KEY"]))
