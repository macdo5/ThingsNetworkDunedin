import datetime
import json
from pymongo import MongoClient
from bson import json_util

client = MongoClient()
db = client.duniot_database
node_data = db.node_data

class DataEntry:

    def __init__(self, mongoData):
        self.data = {}
        self.data["data"] = data
        self.data["gwTime"] = time

class NodeEntry:

    def __init__(self, mongoNode, dataEntries):
        self.data = {}  # create an empty object
        self.data['applicationName'] = mongoNode["applicationName"]
        self.data['nodeName'] = mongoNode["nodeName"]
        self.data['dataEntries'] = dataEntries
        self.data['applicationID'] = mongoNode["applicationID"]
        self.data['devEUI'] = mongoNode["devEUI"]

    def get_latest_date(self):
        now = datetime.datetime.now()
        return max(date for date in self.data["dataEntries"]["gwTime"] if date < now)

    def get_earliest_date(self):
        return min(self.data["dataEntries"]["gwTime"])


class DataEntry:

    def __init__(self, mongoData):
        self.data = {}
        self.data["data"] = mongoData["data"]
        self.data["gwTime"] = mongoData["time"]


def get_node(devEUI):
    found = node_data.find({
        "devEUI": devEUI
    }).limit(1)
    if found.count() != 0:
        jsonFound = json_util.loads(json_util.dumps(found))[0]
        dataEntries =
        node = NodeEntry(jsonFound)
        return node
