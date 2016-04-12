# Change Log
All notable changes to this project will be documented in this file.

## Upcoming Release
###
-
## [2.4.0.12] - 2016-04-06
### Fixed
- Add xml comments to nuget package (Submitted by vongillern)

## [2.4.0.11] - 2015-04-23
### Fixed
- HL7 Version 2.5.1 messages would not parse due to having incorrect namespaces.  Affected segments "DT, ST and TM". (Reported by lharless)

## [2.4.0.10] - 2015-03-15
### Fixed
- HL7 Version 2.1 messages would not parse due to not being listed as an acceptable base version. (Reported by taiji123)

## [2.4.0.9] - 2015-03-03
### Fixed
- Catch unhandled InvalidOperationExceptions when populating a HL7Exception's ERR segment using HL7Exception.populate().

## [2.4.0.8] - 2015-03-02
### Fixed
- Resolve EventMaps not being loaded properly.
- Update FT's SizeRule to be up to 64k in length.
- Include field offset component when rethrowing caught DataTypeExceptions whilst parsing.

## [2.4.0.7] - 2015-02-21
### Fixed
- Abstract and Varies segments were not having their Description field values populated. (Reported bv TheStephenStanton)

## [2.4.0.6] - 2015-02-21
### Fixed
- ORU_R01 messages for v2.5.1 incorrectly did not support the repeating, optional segments OBX, Timing QTY and Specimen.
- Moved regular expression field validation into it's own StrictValidation opt in validation context.
- Correctly escape \X0A\ as \n (thanks to riaz.ahmad)
- Each field / token was being extracted twice (Reported by AFAde)

## [2.4.0.5] - 2015-02-19
### Fixed
- Nuget Package wasn't deploying the 2.51 dll due to new nuget command line packaging and deployment.
