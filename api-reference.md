# DocXml.dll v.3.1.4.0 API documentation

# All types

[CommonComments class](#commoncomments-class)

[DocXmlReader class](#docxmlreader-class)

[DocXmlReaderExtensions class](#docxmlreaderextensions-class)

[EnumComments class](#enumcomments-class)

[EnumValueComment class](#enumvaluecomment-class)

[InheritdocTag class](#inheritdoctag-class)

[MethodComments class](#methodcomments-class)

[ReflectionExtensions class](#reflectionextensions-class)

[ReflectionSettings class](#reflectionsettings-class)

[TypeCollection class](#typecollection-class)

[TypeComments class](#typecomments-class)

[TypeInformation class](#typeinformation-class)

[XmlDocId class](#xmldocid-class)



# ReflectionExtensions Class

Namespace: DocXml.Reflection

Reflection extension methods with supporting properties.

## Properties

| Name | Type | Summary |
|---|---|---|
| **KnownTypeNames** | Dictionary\<Type, string\> | Dictionary containing mapping of type to type names. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **CreateKnownTypeNamesDictionary()** | Dictionary\<Type, string\> | Creates default dictionary of standard value types plus string type.  |
| **IsNullable(Type type)** | bool | Check if this is nullable type.  |
| **ToNameString(Type type, Func\<Type, string\> typeNameConverter)** | string | Convert type to the proper type name.<br>Optional **typeNameConverter** function can convert type names<br>to strings if type names should be decorated in some way either by adding links<br>or formatting.<br><br>This method returns ValueTuple types without field names.  |
| **ToNameString(Type type, Func\<Type, Queue\<string\>, string\> typeNameConverter)** | string | Convert type to the proper type name.<br>Optional **typeNameConverter** function can convert type names<br>to strings if type names should be decorated in some way either by adding links<br>or formatting.<br><br>This method returns ValueTuple types without field names.  |
| **ToNameString(Type type, Queue\<string\> tupleFieldNames, Func\<Type, Queue\<string\>, string\> typeNameConverter)** | string | Convert type to the proper type name.<br>Optional **typeNameConverter** function can convert type names<br>to strings if type names should be decorated in some way either by adding links<br>or formatting.<br><br>This method returns named tuples with field names like this (Type1 field1, Type2 field2).  **tupleFieldNames** parameter<br>must be specified with all tuple field names stored in the same order as they are in compiler-generated TupleElementNames attribute.<br>If you do not know what it is then the better and easier way is to use ToTypeNameString() methods that retrieve field names from attributes. |
| **ToNameStringWithValueTupleNames(Type type, IList\<string\> tupleNames, Func\<Type, Queue\<string\>, string\> typeNameConverter)** | string | Convert type to the string.<br>Optional **typeNameConverter** function can convert type names<br>to strings if type names should be decorated in some way either by adding links<br>or formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).  |
| **ToParametersString(MethodBase methodInfo, Func\<Type, Queue\<string\>, string\> typeNameConverter)** | string | Convert method parameters to the string. If method has no parameters then returned string is ()<br>If parameters are present then returned string contains parameter names with their type names.<br>Optional **typeNameConverter** function can convert type names<br>to strings if type names should be decorated in some way either by adding links<br>or formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).  |
| **ToTypeNameString(ParameterInfo parameterInfo, Func\<Type, Queue\<string\>, string\> typeNameConverter)** | string | Convert method parameter type to the string.<br>Optional **typeNameConverter** function can convert type names<br>to strings if type names should be decorated in some way either by adding links<br>or formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).  |
| **ToTypeNameString(MethodInfo methodInfo, Func\<Type, Queue\<string\>, string\> typeNameConverter)** | string | Convert method return value type to the string.<br>Optional **typeNameConverter** function can convert type names<br>to strings if type names should be decorated in some way either by adding links<br>or formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).  |
| **ToTypeNameString(PropertyInfo propertyInfo, Func\<Type, Queue\<string\>, string\> typeNameConverter)** | string | Convert property type to the string.<br>Optional **typeNameConverter** function can convert type names<br>to strings if type names should be decorated in some way either by adding links<br>or formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).  |
| **ToTypeNameString(FieldInfo fieldInfo, Func\<Type, Queue\<string\>, string\> typeNameConverter)** | string | Convert field type to the string.<br>Optional **typeNameConverter** function can convert type names<br>to strings if type names should be decorated in some way either by adding links<br>or formatting.<br><br>This method returns ValueTuple types with field names like this (Type1 name1, Type2 name2).  |
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
| **DocXmlReader(XPathDocument xPathDocument, bool unindentText)** | Create reader for specified xpath document. |
| **DocXmlReader(Func\<Assembly, string\> assemblyXmlPathFunction, bool unindentText)** | Open XML documentation files based on assemblies of types. Comment file names <br>are generated based on assembly names by replacing assembly location with .xml. |
| **DocXmlReader(IEnumerable\<Assembly\> assemblies, Func\<Assembly, string\> assemblyXmlPathFunction, bool unindentText)** | Open XML documentation files based on assemblies of types. Comment file names <br>are generated based on assembly names by replacing assembly location with .xml. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **GetEnumComments(Type enumType, bool fillValues)** | [EnumComments](#enumcomments-class) | Get enum type description and comments for enum values. If **fillValues**<br>is false and no comments exist for any value then ValueComments list is empty. |
| **GetMemberComment(MemberInfo memberInfo)** | string | Returns Summary comment for specified class member. |
| **GetMemberComments(MemberInfo memberInfo)** | [CommonComments](#commoncomments-class) | Returns comments for specified class member. |
| **GetMethodComments(MethodBase methodInfo)** | [MethodComments](#methodcomments-class) | Returns comments for the method or constructor. Returns empty comments object<br>if comments for method are missing in XML documentation file.<br>Returned comments tags:<br>Summary, Remarks, Parameters (if present), Responses (if present), Returns |
| **GetMethodComments(MethodBase methodInfo, bool nullIfNoComment)** | [MethodComments](#methodcomments-class) | Returns comments for the class method. May return null object is comments for method<br>are missing in XML documentation file. <br>Returned comments tags:<br>Summary, Remarks, Parameters (if present), Responses (if present), Returns |
| **GetTypeComments(Type type)** | [TypeComments](#typecomments-class) | Return Summary comments for specified type.<br>For Delegate types Parameters field may be returned as well. |
# EnumComments Class

Namespace: LoxSmoke.DocXml

Base class: [CommonComments](#commoncomments-class)

Enum type comments

## Properties

| Name | Type | Summary |
|---|---|---|
| **ValueComments** | List\<[EnumValueComment](#enumvaluecomment-class)\> | "summary" comments of enum values. List contains names, values and <br>comments for each enum value.<br>If none of values have any summary comments then this list may be empty.<br>If at least one value has summary comment then this list contains <br>all enum values with empty comments for values without comments. |
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
| **Parameters** | List\<(string Name, string Text)\> | "param" comments of the method. Each item in the list is the tuple<br>where Item1 is the "name" of the parameter in XML file and <br>Item2 is the body of the comment. |
| **Returns** | string | "returns" comment of the method. |
| **Responses** | List\<(string Code, string Text)\> | "response" comments of the method. The list contains tuples where <br>Item1 is the "code" of the response and<br>Item1 is the body of the comment. |
| **TypeParameters** | List\<(string Name, string Text)\> | "typeparam" comments of the method. Each item in the list is the tuple<br>where Item1 is the "name" of the parameter in XML file and <br>Item2 is the body of the comment. |
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
| **Parameters** | List\<(string Name, string Text)\> | This list contains descriptions of delegate type parameters. <br>For non-delegate types this list is empty.<br>For delegate types this list contains tuples where <br>Item1 is the "param" item "name" attribute and<br>Item2 is the body of the comment |
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
| **EnumValueId(Type enumType, string enumName)** | string | Get XML Id of specified value of the enum type.  |
| **EventId(MemberInfo eventInfo)** | string | Get XML Id of event field |
| **FieldId(MemberInfo fieldInfo)** | string | Get XML Id of field |
| **MemberId(MemberInfo memberInfo)** | string | Get XML Id of any member of the type.  |
| **MethodId(MethodBase methodInfo)** | string | Get XML Id of a class method |
| **PropertyId(MemberInfo propertyInfo)** | string | Get XML Id of property |
| **TypeId(Type type)** | string | Get XML Id of the type definition. |
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

DocXmlReader extension methods to retrieve type properties, methods and fields
using reflection information.

## Methods

| Name | Returns | Summary |
|---|---|---|
| **Comments([DocXmlReader](#docxmlreader-class) reader, IEnumerable\<PropertyInfo\> propInfos)** | IEnumerable\<(PropertyInfo Info, [CommonComments](#commoncomments-class) Comments)\> | Get comments for the collection of properties. |
| **Comments([DocXmlReader](#docxmlreader-class) reader, IEnumerable\<MethodBase\> methodInfos)** | IEnumerable\<(MethodBase Info, [MethodComments](#methodcomments-class) Comments)\> | Get comments for the collection of methods. |
| **Comments([DocXmlReader](#docxmlreader-class) reader, IEnumerable\<FieldInfo\> fieldInfos)** | IEnumerable\<(FieldInfo Info, [CommonComments](#commoncomments-class) Comments)\> | Get comments for the collection of fields. |
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
| **AssemblyFilter** | Func\<Assembly, bool\> | Function that checks if specified types of assembly should be added to the set of the <br>referenced types.<br>Return true if referenced types of the assembly should be examined.<br>Return false if assembly types should be ignored.<br>Default implementation checks if documentation XML file exists for the assembly and if<br>it does then returns true. |
| **TypeFilter** | Func\<Type, bool\> | Checks if specified type should be added to the set of referenced types.<br>Return true if type and types referenced by it should be examined.<br>Function should return false if type should be ignored.<br>Default implementation returns true for all types. |
| **PropertyFilter** | Func\<PropertyInfo, bool\> | Checks if specified property should be added to the list of properties and the<br>set of referenced types.<br>Return true if property and types referenced by it should be examined.<br>Function should return false if property should be ignored.<br>Default implementation returns true for all properties. |
| **MethodFilter** | Func\<MethodBase, bool\> | Checks if specified method should be added to the list of methods and the<br>set of referenced types.<br>Return true if the method and types referenced by it should be examined.<br>Function should return false if method should be ignored.<br>Default implementation returns true for all methods. |
| **FieldFilter** | Func\<FieldInfo, bool\> | Checks if specified field should be added to the list of fields and the<br>set of referenced types.<br>Return true if field and types referenced by it should be examined.<br>Function should return false if field should be ignored.<br>Default implementation returns true for all fields. |
# TypeCollection Class

Namespace: LoxSmoke.DocXml.Reflection

Collection of type information objects.

## Properties

| Name | Type | Summary |
|---|---|---|
| **Settings** | [ReflectionSettings](#reflectionsettings-class) | Reflection settings that should be used when looking for referenced types. |
| **ReferencedTypes** | Dictionary\<Type, [TypeInformation](#typeinformation-class)\> | All referenced types. |
| **VisitedPropTypes** | HashSet\<Type\> | Types that had their data and functions examined. |
| **PendingPropTypes** | Queue\<Type\> | Types that need to have their properties, methods and fields examined. |
| **CheckAssemblies** | Dictionary\<Assembly, bool\> | Cached information from ExamineAssemblies call.<br>Contains the set of assemblies that should be checked or ignored. |
| **IgnoreTypes** | HashSet\<Type\> | Cached information from the ExamineTypes call.<br>Contains the set of types that should be ignored. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| **ForReferencedTypes(Type type, [ReflectionSettings](#reflectionsettings-class) settings)** | [TypeCollection](#typecollection-class) | Get all types referenced by the specified type.<br>Reflection information for the specified type is also returned. |
| **ForReferencedTypes(Assembly assembly, [ReflectionSettings](#reflectionsettings-class) settings)** | [TypeCollection](#typecollection-class) | Get all types referenced by the types from specified assembly. |
| **GetReferencedTypes(Type type, [ReflectionSettings](#reflectionsettings-class) settings)** | void | Get all types referenced by the specified type.<br>Reflection information for the specified type is also returned. |
| **GetReferencedTypes(Assembly assembly, [ReflectionSettings](#reflectionsettings-class) settings)** | void | Get all types referenced by the types from specified assembly. |
| **GetReferencedTypes(IEnumerable\<Assembly\> assemblies, [ReflectionSettings](#reflectionsettings-class) settings)** | void | Get all types referenced by the types from specified assemblies.<br>Reflection information for the specified type is also returned. |
| **UnwrapType(Type parentType, Type type)** | void | Recursively "unwrap" the generic type or array. If type is not generic and not an array<br>then do nothing. |
# TypeInformation Class

Namespace: LoxSmoke.DocXml.Reflection

Reflection information for the class, its methods, properties and fields. 

## Properties

| Name | Type | Summary |
|---|---|---|
| **Type** | Type | The type that this class describes |
| **ReferencesIn** | HashSet\<Type\> | Other types referencing this type. |
| **ReferencesOut** | HashSet\<Type\> | Other types referenced by this type. |
| **Properties** | List\<PropertyInfo\> | The list of property inforation of the class. |
| **Methods** | List\<MethodBase\> | The list of method inforation of the class. |
| **Fields** | List\<FieldInfo\> | The list of field inforation of the class. |
