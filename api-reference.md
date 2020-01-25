# DocXml.dll v.3.3.0.0 API documentation

Created by 
[mddox](https://github.com/loxsmoke/mddox) on 1/25/2020

# All types

|   |   |   |
|---|---|---|
| [ReflectionExtensions Class](#reflectionextensions-class) | [InheritdocTag Class](#inheritdoctag-class) | [ReflectionSettings Class](#reflectionsettings-class) |
| [CommonComments Class](#commoncomments-class) | [MethodComments Class](#methodcomments-class) | [TypeCollection Class](#typecollection-class) |
| [DocXmlReader Class](#docxmlreader-class) | [TypeComments Class](#typecomments-class) | [TypeInformation Class](#typeinformation-class) |
| [EnumComments Class](#enumcomments-class) | [XmlDocId Class](#xmldocid-class) |   |
| [EnumValueComment Class](#enumvaluecomment-class) | [DocXmlReaderExtensions Class](#docxmlreaderextensions-class) |   |
# ReflectionExtensions Class

Namespace: DocXml.Reflection

Reflection extension methods with supporting properties.

## Properties

| Name | Type | Summary |
|---|---|---|
| **KnownTypeNames** | [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), string\> | A dictionary containing a mapping of type to type names. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **CleanGenericTypeName(string genericTypeName)** | string | Remove the parameter count part of the generic type name. <br>For example the generic list type name is List`1.<br>This method leaves only the name part of the type such as List.<br>If specified string does not contain the number of parameters <br>part then the same string is returned. |
| **CreateKnownTypeNamesDictionary()** | [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), string\> | Create a dictionary of standard value types and a string type.  |
| **IsNullable([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)** | bool | Checks if the specified type is a nullable value type. <br>Returns false for object references. |
| **ToNameString([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type, [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), string\> typeNameConverter)** | string | Convert type to the proper type name.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types without field names.  |
| **ToNameString([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type, [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)** | string | Convert type to the proper type name.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types without field names.  |
| **ToNameString([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type, [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)\<string\> tupleFieldNames, [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)** | string | Convert type to the proper type name.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns named tuples with field names like this (Type1 field1, Type2 field2).  **tupleFieldNames** parameter<br>must be specified with all tuple field names stored in the same order as they are in compiler-generated TupleElementNames attribute.<br>If you do not know what it is then the better and easier way is to use ToTypeNameString() methods that retrieve field names from attributes. |
| **ToNameStringWithValueTupleNames([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type, [IList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1)\<string\> tupleNames, [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)** | string | Convert type to the string.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).  |
| **ToParametersString([MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase) methodInfo, [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)** | string | Convert method parameters to the string. If method has no parameters then returned string is ()<br>If parameters are present then returned string contains parameter names with their type names.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).  |
| **ToTypeNameString([ParameterInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.parameterinfo) parameterInfo, [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)** | string | Convert method parameter type to the string.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).  |
| **ToTypeNameString([MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo, [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)** | string | Convert method return value type to the string.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).  |
| **ToTypeNameString([PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) propertyInfo, [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)** | string | Convert property type to the string.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).  |
| **ToTypeNameString([FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo) fieldInfo, [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)\<string\>, string\> typeNameConverter, bool invokeTypeNameConverterForGenericType)** | string | Convert field type to the string.<br>Optional **typeNameConverter** function can convert type names to strings <br>if type names should be decorated in some way either by converting text to markdown or <br>HTML links or adding some formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).  |
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
# DocXmlReader Class

Namespace: LoxSmoke.DocXml

Helper class that reads XML documentation generated by C# compiler from code comments. 

## Properties

| Name | Type | Summary |
|---|---|---|
| **UnIndentText** | bool | Default value is true.<br>When it is set to true DocXmlReader removes leading spaces and an empty<br>lines at the end of the comment.<br>By default XML comments are indented for human readability but it adds<br>leading spaces that are not present in source code.<br>For example here is compiler generated XML documentation with '-' <br>showing spaces for readability. <br>----\<summary\><br>----Text<br>----\</summary\><br>With UnIndentText set to true returned summary text is just "Text"<br>With UnIndentText set to false returned summary text contains leading spaces<br>and the trailing empty line "\n----Text\n----"  |
## Constructors

| Name | Summary |
|---|---|
| **DocXmlReader(string fileName, bool unindentText)** | Create reader and use specified XML documentation file |
| **DocXmlReader([XPathDocument](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xpath.xpathdocument) xPathDocument, bool unindentText)** | Create reader for specified xpath document. |
| **DocXmlReader([Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)\<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly), string\> assemblyXmlPathFunction, bool unindentText)** | Open XML documentation files based on assemblies of types. Comment file names <br>are generated based on assembly names by replacing assembly location with .xml. |
| **DocXmlReader([IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly)\> assemblies, [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)\<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly), string\> assemblyXmlPathFunction, bool unindentText)** | Open XML documentation files based on assemblies of types. Comment file names <br>are generated based on assembly names by replacing assembly location with .xml. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **GetEnumComments([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) enumType, bool fillValues)** | [EnumComments](#enumcomments-class) | Get enum type description and comments for enum values. If **fillValues**<br>is false and no comments exist for any value then ValueComments list is empty. |
| **GetMemberComment([MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo) memberInfo)** | string | Returns Summary comment for specified class member. |
| **GetMemberComments([MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo) memberInfo)** | [CommonComments](#commoncomments-class) | Returns comments for specified class member. |
| **GetMethodComments([MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase) methodInfo)** | [MethodComments](#methodcomments-class) | Returns comments for the method or constructor. Returns empty comments object<br>if comments for method are missing in XML documentation file.<br>Returned comments tags:<br>Summary, Remarks, Parameters (if present), Responses (if present), Returns |
| **GetMethodComments([MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase) methodInfo, bool nullIfNoComment)** | [MethodComments](#methodcomments-class) | Returns comments for the class method. May return null object is comments for method<br>are missing in XML documentation file. <br>Returned comments tags:<br>Summary, Remarks, Parameters (if present), Responses (if present), Returns |
| **GetTypeComments([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)** | [TypeComments](#typecomments-class) | Return Summary comments for specified type.<br>For Delegate types Parameters field may be returned as well. |
# EnumComments Class

Namespace: LoxSmoke.DocXml

Base class: [CommonComments](#commoncomments-class)

Enum type comments

## Properties

| Name | Type | Summary |
|---|---|---|
| **ValueComments** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[EnumValueComment](#enumvaluecomment-class)\> | "summary" comments of enum values. List contains names, values and <br>comments for each enum value.<br>If none of values have any summary comments then this list may be empty.<br>If at least one value has summary comment then this list contains <br>all enum values with empty comments for values without comments. |
| **Summary** | string | "summary" comment |
| **Remarks** | string | "remarks" comment |
| **Example** | string | "example" comment |
| **Inheritdoc** | [InheritdocTag](#inheritdoctag-class) | Inheritdoc tag. Null if missing in comments. |
# EnumValueComment Class

Namespace: LoxSmoke.DocXml

Base class: [CommonComments](#commoncomments-class)

Comment of one enum value

## Properties

| Name | Type | Summary |
|---|---|---|
| **Name** | string | The name of the enum value |
| **Value** | int | Integer value of the enum |
| **Summary** | string | "summary" comment |
| **Remarks** | string | "remarks" comment |
| **Example** | string | "example" comment |
| **Inheritdoc** | [InheritdocTag](#inheritdoctag-class) | Inheritdoc tag. Null if missing in comments. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **ToString()** | string | Debugging-friendly text. |
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
| **Parameters** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<(string Name, string Text)\> | "param" comments of the method. Each item in the list is the tuple<br>where Item1 is the "name" of the parameter in XML file and <br>Item2 is the body of the comment. |
| **Returns** | string | "returns" comment of the method. |
| **Responses** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<(string Code, string Text)\> | "response" comments of the method. The list contains tuples where <br>Item1 is the "code" of the response and<br>Item1 is the body of the comment. |
| **TypeParameters** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<(string Name, string Text)\> | "typeparam" comments of the method. Each item in the list is the tuple<br>where Item1 is the "name" of the parameter in XML file and <br>Item2 is the body of the comment. |
| **Summary** | string | "summary" comment |
| **Remarks** | string | "remarks" comment |
| **Example** | string | "example" comment |
| **Inheritdoc** | [InheritdocTag](#inheritdoctag-class) | Inheritdoc tag. Null if missing in comments. |
# TypeComments Class

Namespace: LoxSmoke.DocXml

Base class: [CommonComments](#commoncomments-class)

Class, Struct or  delegate comments

## Properties

| Name | Type | Summary |
|---|---|---|
| **Parameters** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<(string Name, string Text)\> | This list contains descriptions of delegate type parameters. <br>For non-delegate types this list is empty.<br>For delegate types this list contains tuples where <br>Item1 is the "param" item "name" attribute and<br>Item2 is the body of the comment |
| **Summary** | string | "summary" comment |
| **Remarks** | string | "remarks" comment |
| **Example** | string | "example" comment |
| **Inheritdoc** | [InheritdocTag](#inheritdoctag-class) | Inheritdoc tag. Null if missing in comments. |
# XmlDocId Class

Namespace: LoxSmoke.DocXml

Class that constructs IDs for XML documentation comments.
IDs uniquely identify comments in the XML documentation file.

## Methods

| Name | Returns | Summary |
|---|---|---|
| **EnumValueId([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) enumType, string enumName)** | string | Get XML Id of specified value of the enum type.  |
| **EventId([MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo) eventInfo)** | string | Get XML Id of event field |
| **FieldId([MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo) fieldInfo)** | string | Get XML Id of field |
| **MemberId([MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo) memberInfo)** | string | Get XML Id of any member of the type.  |
| **MethodId([MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase) methodInfo)** | string | Get XML Id of a class method |
| **PropertyId([MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo) propertyInfo)** | string | Get XML Id of property |
| **TypeId([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)** | string | Get XML Id of the type definition. |
## Fields

| Name | Type | Summary |
|---|---|---|
| **MemberPrefix** | char | Type member XML ID prefix. |
| **FieldPrefix** | char | Field name XML ID prefix. |
| **PropertyPrefix** | char | Property name XML ID prefix. |
| **EventPrefix** | char | Event XML ID prefix. |
| **TypePrefix** | char | Type name XML ID prefix. |
| **ConstructorNameID** | string | Part of the constructor XML tag in XML document. |
# DocXmlReaderExtensions Class

Namespace: LoxSmoke.DocXml.Reflection

DocXmlReader extension methods to retrieve type properties, methods, and fields
using reflection information.

## Methods

| Name | Returns | Summary |
|---|---|---|
| **Comments([DocXmlReader](#docxmlreader-class) reader, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)\> propInfos)** | [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<([PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) Info, [CommonComments](#commoncomments-class) Comments)\> | Get comments for the collection of properties. |
| **Comments([DocXmlReader](#docxmlreader-class) reader, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)\> methodInfos)** | [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<([MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase) Info, [MethodComments](#methodcomments-class) Comments)\> | Get comments for the collection of methods. |
| **Comments([DocXmlReader](#docxmlreader-class) reader, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)\> fieldInfos)** | [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<([FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo) Info, [CommonComments](#commoncomments-class) Comments)\> | Get comments for the collection of fields. |
# ReflectionSettings Class

Namespace: LoxSmoke.DocXml.Reflection

Settings used by TypeCollection to retrieve reflection info.

## Properties

| Name | Type | Summary |
|---|---|---|
| **Default** | [ReflectionSettings](#reflectionsettings-class) | Default reflection settings. |
| **PropertyFlags** | BindingFlags | Binding flags to use when retrieving properties of the type. |
| **MethodFlags** | BindingFlags | Binding flags to use when retrieving methods of the type. |
| **FieldFlags** | BindingFlags | Binding flags to use when retrieving fields of the type. |
| **NestedTypeFlags** | BindingFlags | Binding flags to use when retrieving nested types of the type. |
| **AssemblyFilter** | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)\<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly), bool\> | Function that checks if specified types of assembly should be added to the set of the <br>referenced types.<br>Return true if referenced types of the assembly should be examined.<br>Return false if assembly types should be ignored.<br>Default implementation checks if documentation XML file exists for the assembly and if<br>it does then returns true. |
| **TypeFilter** | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), bool\> | Checks if specified type should be added to the set of referenced types.<br>Return true if type and types referenced by it should be examined.<br>Function should return false if type should be ignored.<br>Default implementation returns true for all types. |
| **PropertyFilter** | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)\<[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo), bool\> | Checks if specified property should be added to the list of properties and the<br>set of referenced types.<br>Return true if property and types referenced by it should be examined.<br>Function should return false if property should be ignored.<br>Default implementation returns true for all properties. |
| **MethodFilter** | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)\<[MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase), bool\> | Checks if specified method should be added to the list of methods and the<br>set of referenced types.<br>Return true if the method and types referenced by it should be examined.<br>Function should return false if method should be ignored.<br>Default implementation returns true for all methods. |
| **FieldFilter** | [Func](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)\<[FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo), bool\> | Checks if specified field should be added to the list of fields and the<br>set of referenced types.<br>Return true if field and types referenced by it should be examined.<br>Function should return false if field should be ignored.<br>Default implementation returns true for all fields. |
# TypeCollection Class

Namespace: LoxSmoke.DocXml.Reflection

Collection of type information objects.

## Properties

| Name | Type | Summary |
|---|---|---|
| **Settings** | [ReflectionSettings](#reflectionsettings-class) | Reflection settings that should be used when looking for referenced types. |
| **ReferencedTypes** | [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type), [TypeInformation](#typeinformation-class)\> | All referenced types. |
| **VisitedPropTypes** | [HashSet](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)\> | Types that had their data and functions examined. |
| **PendingPropTypes** | [Queue](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)\> | Types that need to have their properties, methods and fields examined. |
| **CheckAssemblies** | [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)\<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly), bool\> | Cached information from ExamineAssemblies call.<br>Contains the set of assemblies that should be checked or ignored. |
| **IgnoreTypes** | [HashSet](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)\> | Cached information from the ExamineTypes call.<br>Contains the set of types that should be ignored. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **ForReferencedTypes([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type, [ReflectionSettings](#reflectionsettings-class) settings)** | [TypeCollection](#typecollection-class) | Get all types referenced by the specified type.<br>Reflection information for the specified type is also returned. |
| **ForReferencedTypes([Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly) assembly, [ReflectionSettings](#reflectionsettings-class) settings)** | [TypeCollection](#typecollection-class) | Get all types referenced by the types from specified assembly. |
| **ForReferencedTypes([IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly)\> assemblies, [ReflectionSettings](#reflectionsettings-class) settings)** | [TypeCollection](#typecollection-class) | Get all types referenced by the types from the list of assemblies. |
| **GetReferencedTypes([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type, [ReflectionSettings](#reflectionsettings-class) settings)** | void | Get all types referenced by the specified type.<br>Reflection information for the specified type is also returned. |
| **GetReferencedTypes([Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly) assembly, [ReflectionSettings](#reflectionsettings-class) settings)** | void | Get all types referenced by the types from specified assembly. |
| **GetReferencedTypes([IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly)\> assemblies, [ReflectionSettings](#reflectionsettings-class) settings)** | void | Get all types referenced by the types from specified assemblies.<br>Reflection information for the specified type is also returned. |
| **UnwrapType([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) parentType, [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)** | void | Recursively "unwrap" the generic type or array. If type is not generic and not an array<br>then do nothing. |
# TypeInformation Class

Namespace: LoxSmoke.DocXml.Reflection

Reflection information for the class, its methods, properties and fields. 

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) | The type that this class describes |
| **ReferencesIn** | [HashSet](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)\> | Other types referencing this type. |
| **ReferencesOut** | [HashSet](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1)\<[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)\> | Other types referenced by this type. |
| **Properties** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)\> | The list of property inforation of the class. |
| **Methods** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)\> | The list of method inforation of the class. |
| **Fields** | [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)\<[FieldInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)\> | The list of field inforation of the class. |
