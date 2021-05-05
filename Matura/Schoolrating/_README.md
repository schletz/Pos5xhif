# Probeklausur für POS: Lehrerbewertung

## Umgebung

Im Labor steht Visual Studio 2017 mit der .NET Core Version 2.1.520 zur Verfügung. Die C# Sprachversion
ist 7.3. Daher können keine nullable reference Types oder records verwendet werden. Alle erstellten
C# Projektdateien (csproj) müssen sich daher auf .NET Core 2.1 beziehen:

```xml
  <PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
  </PropertyGroup>
```

## Musterprojekt

In der Datei [Spg_Schoolrating.sln](Spg_Schoolrating.sln) ist
eine Solution auf Basis von .NET Core 2.1 gespeichert, die die grundlegende Aufteilung der Arbeit
schon beinhaltet:

- Projekt Spg_Schoolrating.Application: Domainklassen, Services und Infrastruktur (DbContext)
- Projekt Spg_Schoolrating.Test: Projekt für die xUnit Tests
- Projekt Spg_Schoolrating.Rest: Projekt für die ASP.NET Core 2.1 REST API

## Intro

```plantuml
class Address {
    + Street : string
    + Zip : string
    + City : string
}
class Name {
    + Title : string
    + Firstname : string
    + Lastname : string
}
class Rating {
    + Id : int
    + Value : int
    + RatingUpdated : DateTime
}
Rating --> DateTime
class RatingCategory {
    + Id : int
    + Name : string
}
class School {
    + Id : int
    + SchoolNumber : int
    + Name : string
    + PupilsCount : int
}

School --> Address
School --> SchoolType

class SchoolRating {
    + SchoolId : int
    + RatingCategoryId : int
}
Rating <|-- SchoolRating
SchoolRating --> School
SchoolRating --> SchoolRatingCategory

class SchoolRatingCategory {
}
RatingCategory <|-- SchoolRatingCategory
enum SchoolType {
    VS,
    AHS,
    BHS,
    NMS,
}
class Teacher {
    + Id : int
    + Email : string
    + SchoolId : int
}

Teacher --> Name
Teacher --> School

class TeacherRating {
    + TeacherId : int
    + RatingCategoryId : int
}
Rating <|-- TeacherRating
TeacherRating --> Teacher
TeacherRating -->  TeacherRatingCategory
class TeacherRatingCategory {
}
RatingCategory <|-- TeacherRatingCategory
```


## Teilthema Domain Model, O/R Mapping und Persistence


## Teilthema Service Layer / Business Logic



## Teilthema Presentationlayer / REST API

