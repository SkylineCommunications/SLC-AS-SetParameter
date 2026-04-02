# Set Parameter

## About

This script sets a parameter in a DataMiner element. It is designed to be used with GQI (Generic Query Interface) or as a standalone automation script.

## Input Parameters

The script requires the following parameters:

| Name | Description | Format |
|------|-------------|--------|
| Element Identifier | The element containing the table | - Element name (e.g., `MyElement`)<br>- DataMinerID/ElementID (e.g., `123/456`) |
| Parameter Identifier | The parameter ID | A valid integer representing the parameter ID in the element's protocol (e.g., `10`). |
| Value | The value to set | N/A |

These parameters can also be filled in via a GQI query, allowing for dynamic input based on query results.

## Error Handling

The script can return the following error messages:

- The element identifier is empty or invalid
- The table ID is empty, whitespace, or not a valid integer
- The element cannot be found in the DataMiner System
- The element is not started or has not completed its startup routine
- The specified parameter does not exist in the element's protocol
- The user does not have permission to set the parameter
- The script encounters an error while attempting to set the parameter
- An unexpected exception occurs during execution
