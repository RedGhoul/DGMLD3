using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DGMLD3.QuickType.DBMapConversion
{
    public partial class DbMap
    {
        [JsonProperty("?xml")]
        public Xml Xml { get; set; }

        [JsonProperty("DirectedGraph")]
        public DirectedGraph DirectedGraph { get; set; }
    }

    public partial class DirectedGraph
    {
        [JsonProperty("@GraphDirection")]
        public string GraphDirection { get; set; }

        [JsonProperty("@Layout")]
        public string Layout { get; set; }

        [JsonProperty("@ZoomLevel")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ZoomLevel { get; set; }

        [JsonProperty("@xmlns")]
        public Uri Xmlns { get; set; }

        [JsonProperty("Nodes")]
        public Nodes Nodes { get; set; }

        [JsonProperty("Links")]
        public Links Links { get; set; }

        [JsonProperty("Categories")]
        public Categories Categories { get; set; }

        [JsonProperty("Properties")]
        public Properties Properties { get; set; }

        [JsonProperty("Styles")]
        public Styles Styles { get; set; }
    }

    public partial class Categories
    {
        [JsonProperty("Category")]
        public Category[] Category { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("@Id")]
        public Id Id { get; set; }

        [JsonProperty("@Label", NullValueHandling = NullValueHandling.Ignore)]
        public Label? Label { get; set; }

        [JsonProperty("@Description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("@CanBeDataDriven", NullValueHandling = NullValueHandling.Ignore)]
        public string CanBeDataDriven { get; set; }

        [JsonProperty("@CanLinkedNodesBeDataDriven", NullValueHandling = NullValueHandling.Ignore)]
        public CanLinkedNodesBeDataDriven? CanLinkedNodesBeDataDriven { get; set; }

        [JsonProperty("@IncomingActionLabel", NullValueHandling = NullValueHandling.Ignore)]
        public string IncomingActionLabel { get; set; }

        [JsonProperty("@IsContainment", NullValueHandling = NullValueHandling.Ignore)]
        public CanLinkedNodesBeDataDriven? IsContainment { get; set; }

        [JsonProperty("@OutgoingActionLabel", NullValueHandling = NullValueHandling.Ignore)]
        public Label? OutgoingActionLabel { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("Link")]
        public Link[] Link { get; set; }
    }

    public partial class Link
    {
        [JsonProperty("@Source")]
        public string Source { get; set; }

        [JsonProperty("@Target")]
        public string Target { get; set; }

        [JsonProperty("@Category")]
        public Label Category { get; set; }

        [JsonProperty("@Label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty("@Bounds", NullValueHandling = NullValueHandling.Ignore)]
        public string Bounds { get; set; }

        [JsonProperty("@LabelBounds", NullValueHandling = NullValueHandling.Ignore)]
        public string LabelBounds { get; set; }
    }

    public partial class Nodes
    {
        [JsonProperty("Node")]
        public Node[] Node { get; set; }
    }

    public partial class Node
    {
        [JsonProperty("@Id")]
        public string Id { get; set; }

        [JsonProperty("@Category")]
        public Id Category { get; set; }

        [JsonProperty("@Bounds", NullValueHandling = NullValueHandling.Ignore)]
        public string Bounds { get; set; }

        [JsonProperty("@Group", NullValueHandling = NullValueHandling.Ignore)]
        public Group? Group { get; set; }

        [JsonProperty("@IsHubContainer", NullValueHandling = NullValueHandling.Ignore)]
        public CanLinkedNodesBeDataDriven? IsHubContainer { get; set; }

        [JsonProperty("@Label")]
        public string Label { get; set; }

        [JsonProperty("@Description", NullValueHandling = NullValueHandling.Ignore)]
        public Description? Description { get; set; }

        [JsonProperty("@UseManualLocation", NullValueHandling = NullValueHandling.Ignore)]
        public CanLinkedNodesBeDataDriven? UseManualLocation { get; set; }

        [JsonProperty("@ErrorMessage", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorMessage? ErrorMessage { get; set; }

        [JsonProperty("@Hub", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Hub { get; set; }
    }

    public partial class Properties
    {
        [JsonProperty("Property")]
        public Property[] Property { get; set; }
    }

    public partial class Property
    {
        [JsonProperty("@Id")]
        public string Id { get; set; }

        [JsonProperty("@DataType")]
        public string DataType { get; set; }

        [JsonProperty("@Label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty("@Description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }

    public partial class Styles
    {
        [JsonProperty("Style")]
        public Style[] Style { get; set; }
    }

    public partial class Style
    {
        [JsonProperty("@TargetType")]
        public string TargetType { get; set; }

        [JsonProperty("@GroupLabel")]
        public Id GroupLabel { get; set; }

        [JsonProperty("@ToolTip", NullValueHandling = NullValueHandling.Ignore)]
        public string ToolTip { get; set; }

        [JsonProperty("@ValueLabel")]
        public string ValueLabel { get; set; }

        [JsonProperty("Condition")]
        public Condition Condition { get; set; }

        [JsonProperty("Setter")]
        public SetterUnion Setter { get; set; }
    }

    public partial class Condition
    {
        [JsonProperty("@Expression")]
        public string Expression { get; set; }
    }

    public partial class SetterElement
    {
        [JsonProperty("@Property")]
        public PropertyEnum Property { get; set; }

        [JsonProperty("@Value")]
        public string Value { get; set; }
    }

    public partial class Xml
    {
        [JsonProperty("@version")]
        public string Version { get; set; }

        [JsonProperty("@encoding")]
        public string Encoding { get; set; }
    }

    public enum CanLinkedNodesBeDataDriven { True };

    public enum Id { Contains, Database, Field, FieldForeign, FieldOptional, FieldPrimary, ForeignKey, Hub, Schema, Table, Model, EntityType, PropertyOptional, PropertyForeign, NavigationProperty, NavigationCollection, PropertyPrimary, PropertyRequired };

    public enum Label { Contains, ForeignKey };

    public enum Description { Bigint, Bit, Datetime, Float, Image, Int, Money, Nchar1, Nchar10, Nchar20, Nchar5, Ntext, Numeric162, Numeric180, Numeric182, Numeric184, Numeric188, Numeric22, Numeric382, Nvarchar1, Nvarchar10, Nvarchar100, Nvarchar1000, Nvarchar120, Nvarchar128, Nvarchar15, Nvarchar150, Nvarchar1500, Nvarchar2, Nvarchar20, Nvarchar200, Nvarchar2000, Nvarchar250, Nvarchar2500, Nvarchar254, Nvarchar255, Nvarchar3, Nvarchar300, Nvarchar40, Nvarchar400, Nvarchar4000, Nvarchar450, Nvarchar5, Nvarchar50, Nvarchar500, Nvarchar6, Uniqueidentifier };

    public enum ErrorMessage { NodeIdHasAHubValueOf0, NodeIdHasAHubValueOf1 };

    public enum Group { Collapsed, Expanded };

    public enum PropertyEnum { Background, Stroke, StrokeThickness };

    public partial struct SetterUnion
    {
        public SetterElement SetterElement;
        public SetterElement[] SetterElementArray;

        public static implicit operator SetterUnion(SetterElement SetterElement) => new SetterUnion { SetterElement = SetterElement };
        public static implicit operator SetterUnion(SetterElement[] SetterElementArray) => new SetterUnion { SetterElementArray = SetterElementArray };
    }

    public partial class DbMap
    {
        public static DbMap FromJson(string json) => JsonConvert.DeserializeObject<DbMap>(json, DBMapConversion.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this DbMap self) => JsonConvert.SerializeObject(self, DBMapConversion.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                CanLinkedNodesBeDataDrivenConverter.Singleton,
                IdConverter.Singleton,
                LabelConverter.Singleton,
                DescriptionConverter.Singleton,
                ErrorMessageConverter.Singleton,
                GroupConverter.Singleton,
                SetterUnionConverter.Singleton,
                PropertyEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class CanLinkedNodesBeDataDrivenConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(CanLinkedNodesBeDataDriven) || t == typeof(CanLinkedNodesBeDataDriven?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "True")
            {
                return CanLinkedNodesBeDataDriven.True;
            }
            throw new Exception("Cannot unmarshal type CanLinkedNodesBeDataDriven");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (CanLinkedNodesBeDataDriven)untypedValue;
            if (value == CanLinkedNodesBeDataDriven.True)
            {
                serializer.Serialize(writer, "True");
                return;
            }
            throw new Exception("Cannot marshal type CanLinkedNodesBeDataDriven");
        }

        public static readonly CanLinkedNodesBeDataDrivenConverter Singleton = new CanLinkedNodesBeDataDrivenConverter();
    }

    internal class IdConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Id) || t == typeof(Id?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Contains":
                    return Id.Contains;
                case "Database":
                    return Id.Database;
                case "Field":
                    return Id.Field;
                case "Field Foreign":
                    return Id.FieldForeign;
                case "Field Optional":
                    return Id.FieldOptional;
                case "Field Primary":
                    return Id.FieldPrimary;
                case "Foreign Key":
                    return Id.ForeignKey;
                case "Hub":
                    return Id.Hub;
                case "Schema":
                    return Id.Schema;
                case "Table":
                    return Id.Table;
                case "Model":
                    return Id.Model;
                case "EntityType":
                    return Id.EntityType;
                case "Navigation Collection":
                    return Id.NavigationCollection;
                case "Navigation Property":
                    return Id.NavigationProperty;
                case "Property Foreign":
                    return Id.PropertyForeign;
                case "Property Optional":
                    return Id.PropertyOptional;
                case "Property Primary":
                    return Id.PropertyPrimary;
                case "Property Required":
                    return Id.PropertyRequired;
            }
            throw new Exception("Cannot unmarshal type Id");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Id)untypedValue;
            switch (value)
            {
                case Id.Contains:
                    serializer.Serialize(writer, "Contains");
                    return;
                case Id.Database:
                    serializer.Serialize(writer, "Database");
                    return;
                case Id.Field:
                    serializer.Serialize(writer, "Field");
                    return;
                case Id.FieldForeign:
                    serializer.Serialize(writer, "Field Foreign");
                    return;
                case Id.FieldOptional:
                    serializer.Serialize(writer, "Field Optional");
                    return;
                case Id.FieldPrimary:
                    serializer.Serialize(writer, "Field Primary");
                    return;
                case Id.ForeignKey:
                    serializer.Serialize(writer, "Foreign Key");
                    return;
                case Id.Hub:
                    serializer.Serialize(writer, "Hub");
                    return;
                case Id.Schema:
                    serializer.Serialize(writer, "Schema");
                    return;
                case Id.Table:
                    serializer.Serialize(writer, "Table");
                    return;
                case Id.EntityType:
                    serializer.Serialize(writer, "EntityType");
                    return;
                case Id.Model:
                    serializer.Serialize(writer, "Model");
                    return;
                case Id.NavigationCollection:
                    serializer.Serialize(writer, "Navigation Collection");
                    return;
                case Id.NavigationProperty:
                    serializer.Serialize(writer, "Navigation Property");
                    return;
                case Id.PropertyForeign:
                    serializer.Serialize(writer, "Property Foreign");
                    return;
                case Id.PropertyOptional:
                    serializer.Serialize(writer, "Property Optional");
                    return;
                case Id.PropertyPrimary:
                    serializer.Serialize(writer, "Property Primary");
                    return;
                case Id.PropertyRequired:
                    serializer.Serialize(writer, "Property Required");
                    return;
            }
            throw new Exception("Cannot marshal type Id");
        }

        public static readonly IdConverter Singleton = new IdConverter();
    }

    internal class LabelConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Label) || t == typeof(Label?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Contains":
                    return Label.Contains;
                case "Foreign Key":
                    return Label.ForeignKey;
            }
            throw new Exception("Cannot unmarshal type Label");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Label)untypedValue;
            switch (value)
            {
                case Label.Contains:
                    serializer.Serialize(writer, "Contains");
                    return;
                case Label.ForeignKey:
                    serializer.Serialize(writer, "Foreign Key");
                    return;
            }
            throw new Exception("Cannot marshal type Label");
        }

        public static readonly LabelConverter Singleton = new LabelConverter();
    }

    internal class DescriptionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Description) || t == typeof(Description?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "bigint":
                    return Description.Bigint;
                case "bit":
                    return Description.Bit;
                case "datetime":
                    return Description.Datetime;
                case "float":
                    return Description.Float;
                case "image":
                    return Description.Image;
                case "int":
                    return Description.Int;
                case "money":
                    return Description.Money;
                case "nchar(1)":
                    return Description.Nchar1;
                case "nchar(10)":
                    return Description.Nchar10;
                case "nchar(20)":
                    return Description.Nchar20;
                case "nchar(5)":
                    return Description.Nchar5;
                case "ntext":
                    return Description.Ntext;
                case "numeric(16,2)":
                    return Description.Numeric162;
                case "numeric(18,0)":
                    return Description.Numeric180;
                case "numeric(18,2)":
                    return Description.Numeric182;
                case "numeric(18,4)":
                    return Description.Numeric184;
                case "numeric(18,8)":
                    return Description.Numeric188;
                case "numeric(2,2)":
                    return Description.Numeric22;
                case "numeric(38,2)":
                    return Description.Numeric382;
                case "nvarchar(1)":
                    return Description.Nvarchar1;
                case "nvarchar(10)":
                    return Description.Nvarchar10;
                case "nvarchar(100)":
                    return Description.Nvarchar100;
                case "nvarchar(1000)":
                    return Description.Nvarchar1000;
                case "nvarchar(120)":
                    return Description.Nvarchar120;
                case "nvarchar(128)":
                    return Description.Nvarchar128;
                case "nvarchar(15)":
                    return Description.Nvarchar15;
                case "nvarchar(150)":
                    return Description.Nvarchar150;
                case "nvarchar(1500)":
                    return Description.Nvarchar1500;
                case "nvarchar(2)":
                    return Description.Nvarchar2;
                case "nvarchar(20)":
                    return Description.Nvarchar20;
                case "nvarchar(200)":
                    return Description.Nvarchar200;
                case "nvarchar(2000)":
                    return Description.Nvarchar2000;
                case "nvarchar(250)":
                    return Description.Nvarchar250;
                case "nvarchar(2500)":
                    return Description.Nvarchar2500;
                case "nvarchar(254)":
                    return Description.Nvarchar254;
                case "nvarchar(255)":
                    return Description.Nvarchar255;
                case "nvarchar(3)":
                    return Description.Nvarchar3;
                case "nvarchar(300)":
                    return Description.Nvarchar300;
                case "nvarchar(40)":
                    return Description.Nvarchar40;
                case "nvarchar(400)":
                    return Description.Nvarchar400;
                case "nvarchar(4000)":
                    return Description.Nvarchar4000;
                case "nvarchar(450)":
                    return Description.Nvarchar450;
                case "nvarchar(5)":
                    return Description.Nvarchar5;
                case "nvarchar(50)":
                    return Description.Nvarchar50;
                case "nvarchar(500)":
                    return Description.Nvarchar500;
                case "nvarchar(6)":
                    return Description.Nvarchar6;
                case "uniqueidentifier":
                    return Description.Uniqueidentifier;
            }
            throw new Exception("Cannot unmarshal type Description");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Description)untypedValue;
            switch (value)
            {
                case Description.Bigint:
                    serializer.Serialize(writer, "bigint");
                    return;
                case Description.Bit:
                    serializer.Serialize(writer, "bit");
                    return;
                case Description.Datetime:
                    serializer.Serialize(writer, "datetime");
                    return;
                case Description.Float:
                    serializer.Serialize(writer, "float");
                    return;
                case Description.Image:
                    serializer.Serialize(writer, "image");
                    return;
                case Description.Int:
                    serializer.Serialize(writer, "int");
                    return;
                case Description.Money:
                    serializer.Serialize(writer, "money");
                    return;
                case Description.Nchar1:
                    serializer.Serialize(writer, "nchar(1)");
                    return;
                case Description.Nchar10:
                    serializer.Serialize(writer, "nchar(10)");
                    return;
                case Description.Nchar20:
                    serializer.Serialize(writer, "nchar(20)");
                    return;
                case Description.Nchar5:
                    serializer.Serialize(writer, "nchar(5)");
                    return;
                case Description.Ntext:
                    serializer.Serialize(writer, "ntext");
                    return;
                case Description.Numeric162:
                    serializer.Serialize(writer, "numeric(16,2)");
                    return;
                case Description.Numeric180:
                    serializer.Serialize(writer, "numeric(18,0)");
                    return;
                case Description.Numeric182:
                    serializer.Serialize(writer, "numeric(18,2)");
                    return;
                case Description.Numeric184:
                    serializer.Serialize(writer, "numeric(18,4)");
                    return;
                case Description.Numeric188:
                    serializer.Serialize(writer, "numeric(18,8)");
                    return;
                case Description.Numeric22:
                    serializer.Serialize(writer, "numeric(2,2)");
                    return;
                case Description.Numeric382:
                    serializer.Serialize(writer, "numeric(38,2)");
                    return;
                case Description.Nvarchar1:
                    serializer.Serialize(writer, "nvarchar(1)");
                    return;
                case Description.Nvarchar10:
                    serializer.Serialize(writer, "nvarchar(10)");
                    return;
                case Description.Nvarchar100:
                    serializer.Serialize(writer, "nvarchar(100)");
                    return;
                case Description.Nvarchar1000:
                    serializer.Serialize(writer, "nvarchar(1000)");
                    return;
                case Description.Nvarchar120:
                    serializer.Serialize(writer, "nvarchar(120)");
                    return;
                case Description.Nvarchar128:
                    serializer.Serialize(writer, "nvarchar(128)");
                    return;
                case Description.Nvarchar15:
                    serializer.Serialize(writer, "nvarchar(15)");
                    return;
                case Description.Nvarchar150:
                    serializer.Serialize(writer, "nvarchar(150)");
                    return;
                case Description.Nvarchar1500:
                    serializer.Serialize(writer, "nvarchar(1500)");
                    return;
                case Description.Nvarchar2:
                    serializer.Serialize(writer, "nvarchar(2)");
                    return;
                case Description.Nvarchar20:
                    serializer.Serialize(writer, "nvarchar(20)");
                    return;
                case Description.Nvarchar200:
                    serializer.Serialize(writer, "nvarchar(200)");
                    return;
                case Description.Nvarchar2000:
                    serializer.Serialize(writer, "nvarchar(2000)");
                    return;
                case Description.Nvarchar250:
                    serializer.Serialize(writer, "nvarchar(250)");
                    return;
                case Description.Nvarchar2500:
                    serializer.Serialize(writer, "nvarchar(2500)");
                    return;
                case Description.Nvarchar254:
                    serializer.Serialize(writer, "nvarchar(254)");
                    return;
                case Description.Nvarchar255:
                    serializer.Serialize(writer, "nvarchar(255)");
                    return;
                case Description.Nvarchar3:
                    serializer.Serialize(writer, "nvarchar(3)");
                    return;
                case Description.Nvarchar300:
                    serializer.Serialize(writer, "nvarchar(300)");
                    return;
                case Description.Nvarchar40:
                    serializer.Serialize(writer, "nvarchar(40)");
                    return;
                case Description.Nvarchar400:
                    serializer.Serialize(writer, "nvarchar(400)");
                    return;
                case Description.Nvarchar4000:
                    serializer.Serialize(writer, "nvarchar(4000)");
                    return;
                case Description.Nvarchar450:
                    serializer.Serialize(writer, "nvarchar(450)");
                    return;
                case Description.Nvarchar5:
                    serializer.Serialize(writer, "nvarchar(5)");
                    return;
                case Description.Nvarchar50:
                    serializer.Serialize(writer, "nvarchar(50)");
                    return;
                case Description.Nvarchar500:
                    serializer.Serialize(writer, "nvarchar(500)");
                    return;
                case Description.Nvarchar6:
                    serializer.Serialize(writer, "nvarchar(6)");
                    return;
                case Description.Uniqueidentifier:
                    serializer.Serialize(writer, "uniqueidentifier");
                    return;
            }
            throw new Exception("Cannot marshal type Description");
        }

        public static readonly DescriptionConverter Singleton = new DescriptionConverter();
    }

    internal class ErrorMessageConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ErrorMessage) || t == typeof(ErrorMessage?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Node 'Id' has a hub value of 0.":
                    return ErrorMessage.NodeIdHasAHubValueOf0;
                case "Node 'Id' has a hub value of 1.":
                    return ErrorMessage.NodeIdHasAHubValueOf1;
            }
            throw new Exception("Cannot unmarshal type ErrorMessage");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ErrorMessage)untypedValue;
            switch (value)
            {
                case ErrorMessage.NodeIdHasAHubValueOf0:
                    serializer.Serialize(writer, "Node 'Id' has a hub value of 0.");
                    return;
                case ErrorMessage.NodeIdHasAHubValueOf1:
                    serializer.Serialize(writer, "Node 'Id' has a hub value of 1.");
                    return;
            }
            throw new Exception("Cannot marshal type ErrorMessage");
        }

        public static readonly ErrorMessageConverter Singleton = new ErrorMessageConverter();
    }

    internal class GroupConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Group) || t == typeof(Group?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Collapsed":
                    return Group.Collapsed;
                case "Expanded":
                    return Group.Expanded;
            }
            throw new Exception("Cannot unmarshal type Group");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Group)untypedValue;
            switch (value)
            {
                case Group.Collapsed:
                    serializer.Serialize(writer, "Collapsed");
                    return;
                case Group.Expanded:
                    serializer.Serialize(writer, "Expanded");
                    return;
            }
            throw new Exception("Cannot marshal type Group");
        }

        public static readonly GroupConverter Singleton = new GroupConverter();
    }

    internal class SetterUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SetterUnion) || t == typeof(SetterUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<SetterElement>(reader);
                    return new SetterUnion { SetterElement = objectValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<SetterElement[]>(reader);
                    return new SetterUnion { SetterElementArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type SetterUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (SetterUnion)untypedValue;
            if (value.SetterElementArray != null)
            {
                serializer.Serialize(writer, value.SetterElementArray);
                return;
            }
            if (value.SetterElement != null)
            {
                serializer.Serialize(writer, value.SetterElement);
                return;
            }
            throw new Exception("Cannot marshal type SetterUnion");
        }

        public static readonly SetterUnionConverter Singleton = new SetterUnionConverter();
    }

    internal class PropertyEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PropertyEnum) || t == typeof(PropertyEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Background":
                    return PropertyEnum.Background;
                case "Stroke":
                    return PropertyEnum.Stroke;
                case "StrokeThickness":
                    return PropertyEnum.StrokeThickness;
            }
            throw new Exception("Cannot unmarshal type PropertyEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PropertyEnum)untypedValue;
            switch (value)
            {
                case PropertyEnum.Background:
                    serializer.Serialize(writer, "Background");
                    return;
                case PropertyEnum.Stroke:
                    serializer.Serialize(writer, "Stroke");
                    return;
                case PropertyEnum.StrokeThickness:
                    serializer.Serialize(writer, "StrokeThickness");
                    return;
            }
            throw new Exception("Cannot marshal type PropertyEnum");
        }

        public static readonly PropertyEnumConverter Singleton = new PropertyEnumConverter();
    }
}
