# DocXml.dll v3.9.0.0 API documentation

Created by [mddox](https://github.com/loxsmoke/mddox) on 12/1/2025

Command line: mddox.dll DocXml.dll -s latest -c -d

# All types

|   |   |   |
|---|---|---|
| [ReflectionExtensions Class](#reflectionextensions-class) | [CommonComments Class](#commoncomments-class) | [DocXmlReader Class](#docxmlreader-class) |
| [EnumComments Class](#enumcomments-class) | [EnumValueComment Class](#enumvaluecomment-class) | [InheritdocTag Class](#inheritdoctag-class) |
| [MethodComments Class](#methodcomments-class) | [SeeAlsoTag Class](#seealsotag-class) | [TypeComments Class](#typecomments-class) |
| [XmlDocId Class](#xmldocid-class) | [DocXmlReaderExtensions Class](#docxmlreaderextensions-class) | [ReflectionSettings Class](#reflectionsettings-class) |
| [TypeCollection Class](#typecollection-class) | [TypeInformation Class](#typeinformation-class) |   |
# ReflectionExtensions Class

Namespace: DocXml.Reflection

Reflection extension methods with supporting properties.

## Properties

| Name | Type | Summary |
|---|---|---|
| **KnownTypeNames** | [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), string> | A dictionary containing a mapping of primitive types to type names. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| [CleanGenericTypeName(string genericTypeName)](#cleangenerictypenamestring-generictypename) | string | Remove the parameter count part of the generic type name. <br>For example the generic list type name is List`1.<br>This method leaves only the name part of the type such as List.<br>If specified string does not contain the number of parameters <br>part then the same string is returned. |
| [CreateKnownTypeNamesDictionary()](#createknowntypenamesdictionary) | [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), string> | Create a dictionary of primitive value types and a string type. |
| [IsNullable(Type type)](#isnullabletype-type) | bool | Checks if the specified type is a nullable value type. |
| [IsRecord(Type type)](#isrecordtype-type) | bool | Check if specified type is a record type. |
| [ToNameString(Type type, Func<Type, string> typeNameConverter)](#tonamestringtype-type-functype-string-typenameconverter) | string | Convert type to the proper type name.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types without field names. |
| [ToNameString(Type type, Func<Type, Queue<string>, string> typeNameConverter, bool invokeTypeNameConverterForGenericType)](#tonamestringtype-type-functype-queuestring-string-typenameconverter-bool-invoketypenameconverterforgenerictype) | string | Convert type to the proper type name.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types without field names. |
| [ToNameString(Type type, Queue<string> tupleFieldNames, Func<Type, Queue<string>, string> typeNameConverter, bool invokeTypeNameConverterForGenericType)](#tonamestringtype-type-queuestring-tuplefieldnames-functype-queuestring-string-typenameconverter-bool-invoketypenameconverterforgenerictype) | string | Convert type to the proper type name.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns named tuples with field names like this (Type1 field1, Type2 field2).  **tupleFieldNames** parameter<br>must be specified with all tuple field names stored in the same order as they are in compiler-generated TupleElementNames attribute.<br>If you do not know what it is then the better and easier way is to use ToTypeNameString() methods that retrieve field names from attributes. |
| [ToNameStringWithValueTupleNames(Type type, IList<string> tupleNames, Func<Type, Queue<string>, string> typeNameConverter, bool invokeTypeNameConverterForGenericType)](#tonamestringwithvaluetuplenamestype-type-iliststring-tuplenames-functype-queuestring-string-typenameconverter-bool-invoketypenameconverterforgenerictype) | string | Convert type to the string.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2). |
| [ToParametersString(MethodBase methodInfo, Func<Type, Queue<string>, string> typeNameConverter, bool invokeTypeNameConverterForGenericType)](#toparametersstringmethodbase-methodinfo-functype-queuestring-string-typenameconverter-bool-invoketypenameconverterforgenerictype) | string | Convert method parameters to the string. If method has no parameters then returned string is ()<br>If parameters are present then returned string contains parameter names with their type names.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2). |
| [ToTypeNameString(ParameterInfo parameterInfo, Func<Type, Queue<string>, string> typeNameConverter, bool invokeTypeNameConverterForGenericType)](#totypenamestringparameterinfo-parameterinfo-functype-queuestring-string-typenameconverter-bool-invoketypenameconverterforgenerictype) | string | Convert method parameter type to the string.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2). |
| [ToTypeNameString(MethodInfo methodInfo, Func<Type, Queue<string>, string> typeNameConverter, bool invokeTypeNameConverterForGenericType)](#totypenamestringmethodinfo-methodinfo-functype-queuestring-string-typenameconverter-bool-invoketypenameconverterforgenerictype) | string | Convert method return value type to the string.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2). |
| [ToTypeNameString(PropertyInfo propertyInfo, Func<Type, Queue<string>, string> typeNameConverter, bool invokeTypeNameConverterForGenericType)](#totypenamestringpropertyinfo-propertyinfo-functype-queuestring-string-typenameconverter-bool-invoketypenameconverterforgenerictype) | string | Convert property type to the string.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2). |
| [ToTypeNameString(FieldInfo fieldInfo, Func<Type, Queue<string>, string> typeNameConverter, bool invokeTypeNameConverterForGenericType)](#totypenamestringfieldinfo-fieldinfo-functype-queuestring-string-typenameconverter-bool-invoketypenameconverterforgenerictype) | string | Convert field type to the string.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2). |
## Methods

### CleanGenericTypeName(string genericTypeName)

Remove the parameter count part of the generic type name. 
For example the generic list type name is List`1.
This method leaves only the name part of the type such as List.
If specified string does not contain the number of parameters 
part then the same string is returned.

| Parameter | Type | Description |
|---|---|---|
| genericTypeName | string | Type name |


### Returns

string

Type name without the number of parameters.

### CreateKnownTypeNamesDictionary()

Create a dictionary of primitive value types and a string type.



### Returns

[Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), string>

Dictionary mapping types to type names

### IsNullable(Type type)

Checks if the specified type is a nullable value type.

| Parameter | Type | Description |
|---|---|---|
| type | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) | Type to check. |


### Returns

bool

Returns true if the type is nullable like int? or Nullable&lt;T&gt;. False for non-nullable types or object references.

### IsRecord(Type type)

Check if specified type is a record type.

| Parameter | Type | Description |
|---|---|---|
| type | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) |  |


### Returns

bool

Return true if it is a record. False otherwise.

### ToNameString(Type type, Func\<Type, string\> typeNameConverter)

Convert type to the proper type name.
Optional **typeNameConverter** function can convert type names to strings 
if type names should be decorated in some way either by converting text to markdown or 
HTML links or adding some formatting.

This method returns ValueTuple types without field names.

| Parameter | Type | Description |
|---|---|---|
| type | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) | Type information. |
| typeNameConverter | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), string> | The optional function that converts type name to string. |


### Returns

string

Full type name

### ToNameString(Type type, Func\<Type, Queue\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)

Convert type to the proper type name.
Optional **typeNameConverter** function can convert type names to strings 
if type names should be decorated in some way either by converting text to markdown or 
HTML links or adding some formatting.

This method returns ValueTuple types without field names.

| Parameter | Type | Description |
|---|---|---|
| type | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) | Type information. |
| typeNameConverter | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)<string>, string> | The optional function that converts type name to string. |
| invokeTypeNameConverterForGenericType | bool | True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List<SomeType><br>            If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.<br>            If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling <br>            ToNameString() to avoid infinite recursion. |


### Returns

string

Full type name

### ToNameString(Type type, Queue\<string\> tupleFieldNames, Func\<Type, Queue\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)

Convert type to the proper type name.
Optional **typeNameConverter** function can convert type names to strings 
if type names should be decorated in some way either by converting text to markdown or 
HTML links or adding some formatting.

This method returns named tuples with field names like this (Type1 field1, Type2 field2).  **tupleFieldNames** parameter
must be specified with all tuple field names stored in the same order as they are in compiler-generated TupleElementNames attribute.
If you do not know what it is then the better and easier way is to use ToTypeNameString() methods that retrieve field names from attributes.

| Parameter | Type | Description |
|---|---|---|
| type | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) |  |
| tupleFieldNames | [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)<string> | The names of value tuple fields as stored in TupleElementNames attribute. This queue is modified during call. |
| typeNameConverter | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)<string>, string> | The optional function that converts type name to string. |
| invokeTypeNameConverterForGenericType | bool | True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List<SomeType><br>            If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.<br>            If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling <br>            ToNameString() to avoid infinite recursion. |


### Returns

string

Full type name

### ToNameStringWithValueTupleNames(Type type, IList\<string\> tupleNames, Func\<Type, Queue\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)

Convert type to the string.
Optional **typeNameConverter** function can convert type names to strings 
if type names should be decorated in some way either by converting text to markdown or 
HTML links or adding some formatting.

This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).

| Parameter | Type | Description |
|---|---|---|
| type | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) |  |
| tupleNames | [IList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1)<string> | The names of the tuple fields from compiler-generated TupleElementNames attribute |
| typeNameConverter | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)<string>, string> | The optional function that converts type name to string. |
| invokeTypeNameConverterForGenericType | bool | True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List<SomeType><br>            If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.<br>            If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling <br>            ToNameString() to avoid infinite recursion. |


### Returns

string

Full name of the specified type

### ToParametersString(MethodBase methodInfo, Func\<Type, Queue\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)

Convert method parameters to the string. If method has no parameters then returned string is ()
If parameters are present then returned string contains parameter names with their type names.
Optional **typeNameConverter** function can convert type names to strings 
if type names should be decorated in some way either by converting text to markdown or 
HTML links or adding some formatting.

This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).

| Parameter | Type | Description |
|---|---|---|
| methodInfo | [MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase) | Method information |
| typeNameConverter | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)<string>, string> | The optional function that converts type name to string. |
| invokeTypeNameConverterForGenericType | bool | True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List<SomeType><br>            If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.<br>            If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling <br>            ToNameString() to avoid infinite recursion. |


### Returns

string

Full list of parameter types and their names

### ToTypeNameString(ParameterInfo parameterInfo, Func\<Type, Queue\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)

Convert method parameter type to the string.
Optional **typeNameConverter** function can convert type names to strings 
if type names should be decorated in some way either by converting text to markdown or 
HTML links or adding some formatting.

This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).

| Parameter | Type | Description |
|---|---|---|
| parameterInfo | [ParameterInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.parameterinfo) | Parameter information. |
| typeNameConverter | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)<string>, string> | The optional function that converts type name to string. |
| invokeTypeNameConverterForGenericType | bool | True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List<SomeType><br>            If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.<br>            If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling <br>            ToNameString() to avoid infinite recursion. |


### Returns

string

Full type name of the parameter

### ToTypeNameString(MethodInfo methodInfo, Func\<Type, Queue\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)

Convert method return value type to the string.
Optional **typeNameConverter** function can convert type names to strings 
if type names should be decorated in some way either by converting text to markdown or 
HTML links or adding some formatting.

This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).

| Parameter | Type | Description |
|---|---|---|
| methodInfo | [MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) | Method information. |
| typeNameConverter | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)<string>, string> | The optional function that converts type name to string. |
| invokeTypeNameConverterForGenericType | bool | True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List<SomeType><br>            If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.<br>            If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling <br>            ToNameString() to avoid infinite recursion. |


### Returns

string

Full type name of the return value

### ToTypeNameString(PropertyInfo propertyInfo, Func\<Type, Queue\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)

Convert property type to the string.
Optional **typeNameConverter** function can convert type names to strings 
if type names should be decorated in some way either by converting text to markdown or 
HTML links or adding some formatting.

This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).

| Parameter | Type | Description |
|---|---|---|
| propertyInfo | [PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) | Property information. |
| typeNameConverter | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)<string>, string> | The optional function that converts type name to string. |
| invokeTypeNameConverterForGenericType | bool | True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List<SomeType><br>            If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.<br>            If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling <br>            ToNameString() to avoid infinite recursion. |


### Returns

string

Full type name of the property

### ToTypeNameString(FieldInfo fieldInfo, Func\<Type, Queue\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)

Convert field type to the string.
Optional **typeNameConverter** function can convert type names to strings 
if type names should be decorated in some way either by converting text to markdown or 
HTML links or adding some formatting.

This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).

| Parameter | Type | Description |
|---|---|---|
| fieldInfo | [FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo) | Field information. |
| typeNameConverter | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)<string>, string> | The optional function that converts type name to string. |
| invokeTypeNameConverterForGenericType | bool | True if typeNameConverter lambda function should be invoked for generic type name such as for the List name in case of List<SomeType><br>            If the parameter value is false then typeNameConverter is not invoked for the generic type name and only the plain type name is returned.<br>            If the parameter value is true then typeNameConverter must handle generic type definitions carefully and avoid calling <br>            ToNameString() to avoid infinite recursion. |


### Returns

string

Full type name of the field

# CommonComments Class

Namespace: LoxSmoke.DocXml

Base class for comments classes

## Properties

| Name | Type | Summary |
|---|---|---|
| **Summary** | string | "summary" comment |
| **Remarks** | string | "remarks" comment |
| **Example** | string | "example" comment |
| **Inheritdoc** | [InheritdocTag](#inheritdoctag-class) | Inheritdoc tag. Null if missing in comments. |
| **FullCommentText** | string | Full XML comment text |
| **SeeAlso** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<[SeeAlsoTag](#seealsotag-class)> | "seealso" links. |
# DocXmlReader Class

Namespace: LoxSmoke.DocXml

Helper class that reads XML documentation generated by C# compiler from code comments.

## Properties

| Name | Type | Summary |
|---|---|---|
| **UnIndentText** | bool | Default value is true.<br>When it is set to true DocXmlReader removes leading spaces and an empty<br>lines at the end of the comment.<br>By default XML comments are indented for human readability but it adds<br>leading spaces that are not present in source code.<br>For example here is compiler generated XML documentation with '-' <br>showing spaces for readability. <br>----\<summary\><br>----Text<br>----\</summary\><br>With UnIndentText set to true returned summary text is just "Text"<br>With UnIndentText set to false returned summary text contains leading spaces<br>and the trailing empty line "\n----Text\n----" |
## Constructors

| Name | Summary |
|---|---|
| [DocXmlReader(string fileName, bool unindentText)](#docxmlreaderstring-filename-bool-unindenttext) | Create reader and use specified XML documentation file |
| [DocXmlReader(XPathDocument xPathDocument, bool unindentText)](#docxmlreaderxpathdocument-xpathdocument-bool-unindenttext) | Create reader for specified xpath document. |
| [DocXmlReader(Func<Assembly, string> assemblyXmlPathFunction, bool unindentText)](#docxmlreaderfuncassembly-string-assemblyxmlpathfunction-bool-unindenttext) | Open XML documentation files based on assemblies of types. Comment file names <br>are generated based on assembly names by replacing assembly location with .xml. |
| [DocXmlReader(IEnumerable<Assembly> assemblies, Func<Assembly, string> assemblyXmlPathFunction, bool unindentText)](#docxmlreaderienumerableassembly-assemblies-funcassembly-string-assemblyxmlpathfunction-bool-unindenttext) | Open XML documentation files based on assemblies of types. Comment file names <br>are generated based on assembly names by replacing assembly location with .xml. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| [GetEnumComments(Type enumType, bool fillValues)](#getenumcommentstype-enumtype-bool-fillvalues) | [EnumComments](#enumcomments-class) | Get enum type description and comments for enum values. If **fillValues**<br>is false and no comments exist for any value then ValueComments list is empty. |
| [GetMemberComment(MemberInfo memberInfo)](#getmembercommentmemberinfo-memberinfo) | string | Returns Summary comment for specified class member. |
| [GetMemberComments(MemberInfo memberInfo)](#getmembercommentsmemberinfo-memberinfo) | [CommonComments](#commoncomments-class) | Returns comments for specified class member. |
| [GetMethodComments(MethodBase methodInfo)](#getmethodcommentsmethodbase-methodinfo) | [MethodComments](#methodcomments-class) | Returns comments for the method or constructor. Returns empty comments object<br>if comments for method are missing in XML documentation file.<br>Returned comments tags:<br>Summary, Remarks, Parameters (if present), Responses (if present), Returns |
| [GetMethodComments(MethodBase methodInfo, bool nullIfNoComment)](#getmethodcommentsmethodbase-methodinfo-bool-nullifnocomment) | [MethodComments](#methodcomments-class) | Returns comments for the class method. May return null object is comments for method<br>are missing in XML documentation file. <br>Returned comments tags:<br>Summary, Remarks, Parameters (if present), Responses (if present), Returns |
| [GetSeeAlsoTags(XPathNavigator node)](#getseealsotagsxpathnavigator-node) | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<[SeeAlsoTag](#seealsotag-class)> |  |
| [GetTypeComments(Type type)](#gettypecommentstype-type) | [TypeComments](#typecomments-class) | Return Summary comments for specified type.<br>For Delegate types Parameters field may be returned as well. |
## Constructors

### DocXmlReader(string fileName, bool unindentText)

Create reader and use specified XML documentation file

| Parameter | Type | Description |
|---|---|---|
| fileName | string | The name of the XML documentation file. |
| unindentText | bool | True if extra leading spaces should be removed from comments |


### DocXmlReader(XPathDocument xPathDocument, bool unindentText)

Create reader for specified xpath document.

| Parameter | Type | Description |
|---|---|---|
| xPathDocument | [XPathDocument](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xpath.xpathdocument) | XML documentation |
| unindentText | bool | True if extra leading spaces should be removed from comments |


### DocXmlReader(Func\<Assembly, string\> assemblyXmlPathFunction, bool unindentText)

Open XML documentation files based on assemblies of types. Comment file names 
are generated based on assembly names by replacing assembly location with .xml.

| Parameter | Type | Description |
|---|---|---|
| assemblyXmlPathFunction | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly), string> | Function that returns path to the assembly XML comment file.<br>            If function is null then comments file is assumed to have the same file name as assembly.<br>            If function returns null or if comments file does not exist then all comments for types from that |
| unindentText | bool | True if extra leading spaces should be removed from comments |


### DocXmlReader(IEnumerable\<Assembly\> assemblies, Func\<Assembly, string\> assemblyXmlPathFunction, bool unindentText)

Open XML documentation files based on assemblies of types. Comment file names 
are generated based on assembly names by replacing assembly location with .xml.

| Parameter | Type | Description |
|---|---|---|
| assemblies | [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly)> | The list of assemblies for XML documentation |
| assemblyXmlPathFunction | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly), string> | Function that returns path to the assembly XML comment file.<br>            If function is null then comments file is assumed to have the same file name as assembly.<br>            If function returns null or if comments file does not exist then all comments for types from that |
| unindentText | bool | True if extra leading spaces should be removed from comments |


## Methods

### GetEnumComments(Type enumType, bool fillValues)

Get enum type description and comments for enum values. If **fillValues**
is false and no comments exist for any value then ValueComments list is empty.

| Parameter | Type | Description |
|---|---|---|
| enumType | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) | Enum type to get comments for. If this is not an enum type then functions throws an ArgumentException |
| fillValues | bool | True if ValueComments list should be filled even if |


### Returns

[EnumComments](#enumcomments-class)

EnumComment

### GetMemberComment(MemberInfo memberInfo)

Returns Summary comment for specified class member.

| Parameter | Type | Description |
|---|---|---|
| memberInfo | [MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo) |  |


### Returns

string



### GetMemberComments(MemberInfo memberInfo)

Returns comments for specified class member.

| Parameter | Type | Description |
|---|---|---|
| memberInfo | [MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo) |  |


### Returns

[CommonComments](#commoncomments-class)



### GetMethodComments(MethodBase methodInfo)

Returns comments for the method or constructor. Returns empty comments object
if comments for method are missing in XML documentation file.
Returned comments tags:
Summary, Remarks, Parameters (if present), Responses (if present), Returns



### Returns

[MethodComments](#methodcomments-class)



### GetMethodComments(MethodBase methodInfo, bool nullIfNoComment)

Returns comments for the class method. May return null object is comments for method
are missing in XML documentation file. 
Returned comments tags:
Summary, Remarks, Parameters (if present), Responses (if present), Returns

| Parameter | Type | Description |
|---|---|---|
| methodInfo | [MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase) |  |
| nullIfNoComment | bool | Return null if comment for method is not available |


### Returns

[MethodComments](#methodcomments-class)



### GetSeeAlsoTags(XPathNavigator node)





### Returns

[List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<[SeeAlsoTag](#seealsotag-class)>



### GetTypeComments(Type type)

Return Summary comments for specified type.
For Delegate types Parameters field may be returned as well.

| Parameter | Type | Description |
|---|---|---|
| type | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) |  |


### Returns

[TypeComments](#typecomments-class)

TypeComment

# EnumComments Class

Namespace: LoxSmoke.DocXml

Base class: [CommonComments](#commoncomments-class)

Enum type comments

## Properties

| Name | Type | Summary |
|---|---|---|
| **ValueComments** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<[EnumValueComment](#enumvaluecomment-class)> | "summary" comments of enum values. List contains names, values and <br>comments for each enum value.<br>If none of values have any summary comments then this list may be empty.<br>If at least one value has summary comment then this list contains <br>all enum values with empty comments for values without comments. |
| **Summary** | string | "summary" comment |
| **Remarks** | string | "remarks" comment |
| **Example** | string | "example" comment |
| **Inheritdoc** | [InheritdocTag](#inheritdoctag-class) | Inheritdoc tag. Null if missing in comments. |
| **FullCommentText** | string | Full XML comment text |
| **SeeAlso** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<[SeeAlsoTag](#seealsotag-class)> | "seealso" links. |
# EnumValueComment Class

Namespace: LoxSmoke.DocXml

Base class: [CommonComments](#commoncomments-class)

Comment of one enum value

## Properties

| Name | Type | Summary |
|---|---|---|
| **Name** | string | The name of the enum value |
| **Value** | int | Integer value of the enum if enum value fits in signed 32-bit integer.<br>If value is too big (uint, long or ulong) then returned value is 0. |
| **IsBigValue** | bool | True if enum value is too big to fit in int Value property. Use BigValue property instead. |
| **BigValue** | BigInteger | The value of the enum. This field can handle any enum size. |
| **Summary** | string | "summary" comment |
| **Remarks** | string | "remarks" comment |
| **Example** | string | "example" comment |
| **Inheritdoc** | [InheritdocTag](#inheritdoctag-class) | Inheritdoc tag. Null if missing in comments. |
| **FullCommentText** | string | Full XML comment text |
| **SeeAlso** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<[SeeAlsoTag](#seealsotag-class)> | "seealso" links. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| [ToString()](#tostring) | string | Debugging-friendly text. |
## Methods

### ToString()

Debugging-friendly text.



### Returns

string



# InheritdocTag Class

Namespace: LoxSmoke.DocXml

Inheritdoc tag with optional cref attribute.

## Properties

| Name | Type | Summary |
|---|---|---|
| **Cref** | string | Cref attribute value. This value is optional. |
# MethodComments Class

Namespace: LoxSmoke.DocXml

Base class: [CommonComments](#commoncomments-class)

Method, operator and constructor comments

## Properties

| Name | Type | Summary |
|---|---|---|
| **Parameters** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<(string Name, string Text)> | "param" comments of the method. Each item in the list is the tuple where <br>"Name" is the parameter in XML file and <br>"Text" is the body of the comment. |
| **Returns** | string | "returns" comment of the method. |
| **Responses** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<(string Code, string Text)> | "response" comments of the method. The list contains tuples where <br>"Code" is the response code<br>"Text" is the body of the comment. |
| **TypeParameters** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<(string Name, string Text)> | "typeparam" comments of the method. Each item in the list is the tuple where<br>"Name" of the parameter in XML file and <br>"Text" is the body of the comment. |
| **Exceptions** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<(string Cref, string Text)> | "exception" comments of the method or property. Each item in the list is the tuple where<br>"Cref" is the exception type<br>"Text" is the description of the exception |
| **Summary** | string | "summary" comment |
| **Remarks** | string | "remarks" comment |
| **Example** | string | "example" comment |
| **Inheritdoc** | [InheritdocTag](#inheritdoctag-class) | Inheritdoc tag. Null if missing in comments. |
| **FullCommentText** | string | Full XML comment text |
| **SeeAlso** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<[SeeAlsoTag](#seealsotag-class)> | "seealso" links. |
# SeeAlsoTag Class

Namespace: LoxSmoke.DocXml

Seealso tag with optional cref and href attributes.

## Properties

| Name | Type | Summary |
|---|---|---|
| **Cref** | string | Cref attribute value. This value is optional. |
| **Href** | string | Href attribute value. This value is optional. |
| **Text** | string | The title, if any, for this link. |
# TypeComments Class

Namespace: LoxSmoke.DocXml

Base class: [CommonComments](#commoncomments-class)

Class, Struct or  delegate comments

## Properties

| Name | Type | Summary |
|---|---|---|
| **Parameters** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<(string Name, string Text)> | This list contains descriptions of delegate type parameters. <br>For non-delegate types this list is empty.<br>For delegate types this list contains tuples where <br>Name is the "name" attribute of "param"<br>Text is the body of the comment |
| **TypeParameters** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<(string Name, string Text)> | This list contains description of generic type parameter<br>Name is the "name" attribute of "typeparam"<br>Text is the body of the comment |
| **Summary** | string | "summary" comment |
| **Remarks** | string | "remarks" comment |
| **Example** | string | "example" comment |
| **Inheritdoc** | [InheritdocTag](#inheritdoctag-class) | Inheritdoc tag. Null if missing in comments. |
| **FullCommentText** | string | Full XML comment text |
| **SeeAlso** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<[SeeAlsoTag](#seealsotag-class)> | "seealso" links. |
# XmlDocId Class

Namespace: LoxSmoke.DocXml

Class that constructs IDs for XML documentation comments.
IDs uniquely identify comments in the XML documentation file.

## Methods

| Name | Returns | Summary |
|---|---|---|
| [EnumValueId(Type enumType, string enumName)](#enumvalueidtype-enumtype-string-enumname) | string | Get XML Id of specified value of the enum type. |
| [EventId(MemberInfo eventInfo)](#eventidmemberinfo-eventinfo) | string | Get XML Id of event field |
| [FieldId(MemberInfo fieldInfo)](#fieldidmemberinfo-fieldinfo) | string | Get XML Id of field |
| [MemberId(MemberInfo memberInfo)](#memberidmemberinfo-memberinfo) | string | Get XML Id of any member of the type. |
| [MethodId(MethodBase methodInfo)](#methodidmethodbase-methodinfo) | string | Get XML Id of a class method |
| [PropertyId(MemberInfo propertyInfo)](#propertyidmemberinfo-propertyinfo) | string | Get XML Id of property |
| [TypeId(Type type)](#typeidtype-type) | string | Get XML Id of the type definition. |
## Fields

| Name | Type | Summary |
|---|---|---|
| **MemberPrefix** | char | Type member XML ID prefix. |
| **FieldPrefix** | char | Field name XML ID prefix. |
| **PropertyPrefix** | char | Property name XML ID prefix. |
| **EventPrefix** | char | Event XML ID prefix. |
| **TypePrefix** | char | Type name XML ID prefix. |
| **ConstructorNameID** | string | Part of the constructor XML tag in XML document. |
## Methods

### EnumValueId(Type enumType, string enumName)

Get XML Id of specified value of the enum type.

| Parameter | Type | Description |
|---|---|---|
| enumType | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) | Enum type |
| enumName | string | The name of the value without type and namespace |


### Returns

string



### EventId(MemberInfo eventInfo)

Get XML Id of event field

| Parameter | Type | Description |
|---|---|---|
| eventInfo | [MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo) |  |


### Returns

string



### FieldId(MemberInfo fieldInfo)

Get XML Id of field

| Parameter | Type | Description |
|---|---|---|
| fieldInfo | [MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo) |  |


### Returns

string



### MemberId(MemberInfo memberInfo)

Get XML Id of any member of the type.

| Parameter | Type | Description |
|---|---|---|
| memberInfo | [MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo) |  |


### Returns

string



### MethodId(MethodBase methodInfo)

Get XML Id of a class method

| Parameter | Type | Description |
|---|---|---|
| methodInfo | [MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase) |  |


### Returns

string



### PropertyId(MemberInfo propertyInfo)

Get XML Id of property

| Parameter | Type | Description |
|---|---|---|
| propertyInfo | [MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo) |  |


### Returns

string



### TypeId(Type type)

Get XML Id of the type definition.

| Parameter | Type | Description |
|---|---|---|
| type | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) |  |


### Returns

string



# DocXmlReaderExtensions Class

Namespace: LoxSmoke.DocXml.Reflection

DocXmlReader extension methods to retrieve type properties, methods, and fields
using reflection information.

## Methods

| Name | Returns | Summary |
|---|---|---|
| [Comments(DocXmlReader reader, IEnumerable<PropertyInfo> propInfos)](#commentsdocxmlreader-reader-ienumerablepropertyinfo-propinfos) | [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<([PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) Info, [CommonComments](#commoncomments-class) Comments)> | Get comments for the collection of properties. |
| [Comments(DocXmlReader reader, IEnumerable<MethodBase> methodInfos)](#commentsdocxmlreader-reader-ienumerablemethodbase-methodinfos) | [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<([MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase) Info, [MethodComments](#methodcomments-class) Comments)> | Get comments for the collection of methods. |
| [Comments(DocXmlReader reader, IEnumerable<FieldInfo> fieldInfos)](#commentsdocxmlreader-reader-ienumerablefieldinfo-fieldinfos) | [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<([FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo) Info, [CommonComments](#commoncomments-class) Comments)> | Get comments for the collection of fields. |
## Methods

### Comments(DocXmlReader reader, IEnumerable\<PropertyInfo\> propInfos)

Get comments for the collection of properties.

| Parameter | Type | Description |
|---|---|---|
| reader | [DocXmlReader](#docxmlreader-class) |  |
| propInfos | [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)> |  |


### Returns

[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<([PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) Info, [CommonComments](#commoncomments-class) Comments)>



### Comments(DocXmlReader reader, IEnumerable\<MethodBase\> methodInfos)

Get comments for the collection of methods.

| Parameter | Type | Description |
|---|---|---|
| reader | [DocXmlReader](#docxmlreader-class) |  |
| methodInfos | [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<[MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)> |  |


### Returns

[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<([MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase) Info, [MethodComments](#methodcomments-class) Comments)>



### Comments(DocXmlReader reader, IEnumerable\<FieldInfo\> fieldInfos)

Get comments for the collection of fields.

| Parameter | Type | Description |
|---|---|---|
| reader | [DocXmlReader](#docxmlreader-class) |  |
| fieldInfos | [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<[FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)> |  |


### Returns

[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<([FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo) Info, [CommonComments](#commoncomments-class) Comments)>



# ReflectionSettings Class

Namespace: LoxSmoke.DocXml.Reflection

Settings used by TypeCollection to retrieve reflection info.

## Properties

| Name | Type | Summary |
|---|---|---|
| **Default** | [ReflectionSettings](#reflectionsettings-class) | Returns instance of reflection settings with default values.<br>Includes all public and non-public instance and static properties, fields, methods, and types.<br>Returned object can be modified after retrieval to adjust settings. |
| **PropertyFlags** | [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags) | Binding flags to use when retrieving properties of the type. |
| **MethodFlags** | [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags) | Binding flags to use when retrieving methods of the type. |
| **FieldFlags** | [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags) | Binding flags to use when retrieving fields of the type. |
| **NestedTypeFlags** | [BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags) | Binding flags to use when retrieving nested types of the type. |
| **AssemblyFilter** | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly), bool> | Function that checks if specified types of assembly should be added to the set of the <br>referenced types.<br>Return true if referenced types of the assembly should be examined.<br>Return false if assembly types should be ignored.<br>Default implementation checks if documentation XML file exists for the assembly and if<br>it does then returns true. |
| **TypeFilter** | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), bool> | Checks if specified type should be added to the set of referenced types.<br>Return true if type and types referenced by it should be examined.<br>Function should return false if type should be ignored.<br>Default implementation returns true for all types. |
| **PropertyFilter** | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo), bool> | Checks if specified property should be added to the list of properties and the<br>set of referenced types.<br>Return true if property and types referenced by it should be examined.<br>Function should return false if property should be ignored.<br>Default implementation returns true for all properties. |
| **MethodFilter** | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<[MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase), bool> | Checks if specified method should be added to the list of methods and the<br>set of referenced types.<br>Return true if the method and types referenced by it should be examined.<br>Function should return false if method should be ignored.<br>Default implementation returns true for all methods. |
| **FieldFilter** | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<[FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo), bool> | Checks if specified field should be added to the list of fields and the<br>set of referenced types.<br>Return true if field and types referenced by it should be examined.<br>Function should return false if field should be ignored.<br>Default implementation returns true for all fields. |
# TypeCollection Class

Namespace: LoxSmoke.DocXml.Reflection

Collection of type information objects.

## Properties

| Name | Type | Summary |
|---|---|---|
| **Settings** | [ReflectionSettings](#reflectionsettings-class) | Reflection settings that should be used when looking for referenced types. |
| **ReferencedTypes** | [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [TypeInformation](#typeinformation-class)> | All referenced types. |
| **VisitedPropTypes** | [HashSet](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)> | Types that had their data and functions examined. |
| **PendingPropTypes** | [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)> | Types that need to have their properties, methods and fields examined. |
| **CheckAssemblies** | [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly), bool> | Cached information from ExamineAssemblies call.<br>Contains the set of assemblies that should be checked or ignored. |
| **IgnoreTypes** | [HashSet](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)> | Cached information from the ExamineTypes call.<br>Contains the set of types that should be ignored. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| [ForReferencedTypes(Type type, ReflectionSettings settings)](#forreferencedtypestype-type-reflectionsettings-settings) | [TypeCollection](#typecollection-class) | Get all types referenced by the specified type.<br>Reflection information for the specified type is also returned. |
| [ForReferencedTypes(Assembly assembly, ReflectionSettings settings)](#forreferencedtypesassembly-assembly-reflectionsettings-settings) | [TypeCollection](#typecollection-class) | Get all types referenced by the types from specified assembly. |
| [ForReferencedTypes(IEnumerable<Assembly> assemblies, ReflectionSettings settings)](#forreferencedtypesienumerableassembly-assemblies-reflectionsettings-settings) | [TypeCollection](#typecollection-class) | Get all types referenced by the types from the list of assemblies. |
| [GetReferencedTypes(Type type, ReflectionSettings settings)](#getreferencedtypestype-type-reflectionsettings-settings) | void | Get all types referenced by the specified type.<br>Reflection information for the specified type is also returned. |
| [GetReferencedTypes(Assembly assembly, ReflectionSettings settings)](#getreferencedtypesassembly-assembly-reflectionsettings-settings) | void | Get all types referenced by the types from specified assembly. |
| [GetReferencedTypes(IEnumerable<Assembly> assemblies, ReflectionSettings settings)](#getreferencedtypesienumerableassembly-assemblies-reflectionsettings-settings) | void | Get all types referenced by the types from specified assemblies.<br>Reflection information for the specified type is also returned. |
| [UnwrapType(Type parentType, Type type)](#unwraptypetype-parenttype-type-type) | void | Recursively "unwrap" the generic type or array. If type is not generic and not an array<br>then do nothing. |
## Methods

### ForReferencedTypes(Type type, ReflectionSettings settings)

Get all types referenced by the specified type.
Reflection information for the specified type is also returned.

| Parameter | Type | Description |
|---|---|---|
| type | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) |  |
| settings | [ReflectionSettings](#reflectionsettings-class) |  |


### Returns

[TypeCollection](#typecollection-class)



### ForReferencedTypes(Assembly assembly, ReflectionSettings settings)

Get all types referenced by the types from specified assembly.

| Parameter | Type | Description |
|---|---|---|
| assembly | [Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly) |  |
| settings | [ReflectionSettings](#reflectionsettings-class) |  |


### Returns

[TypeCollection](#typecollection-class)



### ForReferencedTypes(IEnumerable\<Assembly\> assemblies, ReflectionSettings settings)

Get all types referenced by the types from the list of assemblies.

| Parameter | Type | Description |
|---|---|---|
| assemblies | [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly)> |  |
| settings | [ReflectionSettings](#reflectionsettings-class) |  |


### Returns

[TypeCollection](#typecollection-class)



### GetReferencedTypes(Type type, ReflectionSettings settings)

Get all types referenced by the specified type.
Reflection information for the specified type is also returned.

| Parameter | Type | Description |
|---|---|---|
| type | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) |  |
| settings | [ReflectionSettings](#reflectionsettings-class) |  |


### GetReferencedTypes(Assembly assembly, ReflectionSettings settings)

Get all types referenced by the types from specified assembly.

| Parameter | Type | Description |
|---|---|---|
| assembly | [Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly) |  |
| settings | [ReflectionSettings](#reflectionsettings-class) |  |


### GetReferencedTypes(IEnumerable\<Assembly\> assemblies, ReflectionSettings settings)

Get all types referenced by the types from specified assemblies.
Reflection information for the specified type is also returned.

| Parameter | Type | Description |
|---|---|---|
| assemblies | [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly)> |  |
| settings | [ReflectionSettings](#reflectionsettings-class) |  |


### UnwrapType(Type parentType, Type type)

Recursively "unwrap" the generic type or array. If type is not generic and not an array
then do nothing.

| Parameter | Type | Description |
|---|---|---|
| parentType | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) |  |
| type | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) |  |


# TypeInformation Class

Namespace: LoxSmoke.DocXml.Reflection

Reflection information for the class, its methods, properties and fields.

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) | The type that this class describes |
| **ReferencesIn** | [HashSet](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)> | Other types referencing this type. |
| **ReferencesOut** | [HashSet](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1)<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)> | Other types referenced by this type. |
| **Properties** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)> | The list of property information of the class. |
| **Methods** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<[MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)> | The list of method information of the class. |
| **Fields** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<[FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)> | The list of field information of the class. |
