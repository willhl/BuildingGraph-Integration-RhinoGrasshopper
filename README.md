
# BuildingGraph Rhino Grasshopper Integration 

BuildingGraph.Integration.RhinoGrasshopper is a C# addin for Rhino Grasshopper to push and pull data from a Neo4j database. There're two sets of nodes provided in this repository:

 - Grasshopper <> Building Graph Server (GraphQL) : Use these for connection to the Building Graph Server (or any other) GraphQL API.
 - WIP: Grasshopper <> Neo4j (bolt) : Use these for direct connection to a Neo4j Database

This repository includes  dependencies to the for Building Graph Neo4j and GraphQL core client libraries: 
[BuildingGraph-Client](https://github.com/willhl/BuildingGraph-Client)

## Building and Installation

Clone this repository to your local system (including submodules): 

    $ git clone --recurse-submodules https://github.com/willhl/BuildingGraph-Integration-RhinoGrasshopper

Or, if you have already cloned this repository, you can grab the submodules with this command:

    $ git submodule update --init --recursive

Open the BuildingGraph-Integration-RhinoGrasshopper.sln solution in Visual Studio and set BuildingGraph.Integration.RhinoGrasshopper as the statup project, select Debug and x64, then hit build, or go straight for run. This will build the project, copy the gha file to the Grasshopper plugins directory, and start Rhino 6.

## Using the Grasshopper <> Building Graph (GraphQL)

The following nodes are currently provided for GraphQL integration:

 - BG Query - Use this to send the Building Graph a GraphQL query, it will return the results as JSON.
 - BG Push - Use this to push a node to the Building Graph
 - BG Relate  - Use this to relate two nodes together

For example Grasshopper graphs using these node see the [ examples repository.](https://github.com/willhl/BuildingGraph-Client-Examples)

## Using the Grasshopper <> Neo4j

Ah, so, this is still WIP, if you're interested in getting this working give me a shout :).