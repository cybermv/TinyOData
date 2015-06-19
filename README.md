# TinyOData
TinyOData is a small library that is used to parse simple OData queries from a given URL to an expression that can be applied on an IQueryable. It will support the following system query options - $skip, $top, $orderby and $filter (basic)

Using the library will be simple and bloat-free - you just add the attribute to the action or controller for which you need to parse the query and put an additional parameter to the action. The attribute will parse and fill this parameter for you and with it you can apply the query to your IQueryable. You can even register the filter globally so you don't have to worry about putting attributes everywhere.

The action parameter will be typed to the entity over which will you apply the parsed query - and the parser will use this type to determine the possible properties and property types over which the filtering and ordering will be available.
