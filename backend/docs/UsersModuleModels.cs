class User : AggregateRoot 
{
    Guid id;
    PersonalInfo personalInfo;
    Set<Trait> Traits;
    Set<Trait> Preferences;
    Location location;
    bool complete;

    AddPreference(Trait preference) 
    {
        // Maybe we should add a preference limit?
        if (!Preferences.Contain(preference))
        {
            Preferences.Add(preference);
            RaiseEvent(new UserAddedPreference(id, preference));
        }
    }

    RemovePreference(Trait preference)
    {
        // Should we have required preferences?
        if (Preferences.Contain(preference)) 
        {
            Preferences.Remove(preference);
            RaiseEvent(new UserRemovedPreference(id, preference));
        }
    }
    
    AddTrait(Trait trait) 
    {
        // Maybe we should add a preference limit?
        if (!Traits.Contain(trait))
        {
            Traits.Add(trait);
            RaiseEvent(new UserAddedTrait(id, trait));
        }
    }

    RemoveTrait(Trait trait)
    {
        // Should we have required preferences?
        if (Traits.Contain(trait)) 
        {
            Traits.Remove(trait);
            RaiseEvent(new UserRemovedTrait(id, trait));
        }
    }

    ChangePersonalInfo(PersonalInfo newPersonalInfo)
    {
        personalInfo = newPersonalInfo;
        complete = personalInfo.IsValid();

        if (complete) 
        {
            RaiseEvent(new UserProfileCompleted(id));
        }
        RaiseEvent(new PersonalInfoChanged(id, personalInfo));
    }
}

struct PersonalInfo 
{
    string Nickname;
    string Description;
    DateTime BirthDate;
    Sex Sex;

    bool IsValid()
    {
        // Description does not contain any forbidden words?
        // Birthdate is in valid range?
        // Sex is defined?
        return !string.IsNullOrWhitespace(Nickname);            
    }
}

struct Location 
{
    double Latitude; 
    double Longitude;

    static Location New(double latitude, double longitude)
    {
        Validate(latitude, longitude);
        Latitude = latitude;
        Longitude = longitude;
    }

    Location Shift(double shiftLatitude, double shiftLongitude)
    {
        return New(latitude + shiftLatitude, longitude + shiftLongitude);
    }

    private void Validate(double latitude, double longitude)
    {
        if (latitude < -90 && latitude > 90)
        {
            throw new InvalidLatitudeException(latitude);
        }

        if (longitude < -180 && longitude > 180)
        {
            throw new InvalidLongitude(longitude);
        }
    }
}

enum Sex 
{
    NotKnown = 0,
    Male = 1,
    Female = 2,
    NotApplicable = 9
}

class Trait : Enity 
{
    int Id;
    string Value;
}
