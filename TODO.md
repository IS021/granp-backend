- [ ] Profile Controller
    - [ ] Comtroller
    - [ ] DTOs
    - [ ] Mapper
    - [ ] Validator

Join the CustomerProfileRequest to the CustomerProfileResponse into CustomerProfileDto?

Files to Fix:

FILTER

CREATE UPDATE DTOS TO HANDLE NULLS AND DIFFERENCES

- DTOs:
    .- CustomerProfileRequest 
    .- CustomerProfileResponse
    .- ProfessionalProfileRequest
    .- ProfessionalProfileResponse
- Mappers:
    - CustomerProfileMapper
    - ProfessionalProfileMapper
- Validators:
    - CustomerProfileValidator
    - ProfessionalProfileValidator

- Repository:
    - ProfessionalProfileRepository (FILTER)
    - GenericRepository (Nulls)
    - GenericUserRepository (Nulls)

- Types:
    - SearchFilter

- Controller:
    - SearchController (FILTER)



FOR NOTIFICATION:

- Modify RESERVATION CONTROLLER