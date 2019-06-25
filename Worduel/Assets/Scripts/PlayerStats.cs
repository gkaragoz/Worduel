using UnityEngine;

[System.Serializable]
public class PlayerStats {

    [SerializeField]
    private string _id = "_id";
    [SerializeField]
    private string _username = "_username";
    [SerializeField]
    private string _firstName = "_firstName";
    [SerializeField]
    private string _lastName = "_lastName";
    [SerializeField]
    private Gender _gender = Gender.Female;

    public string Id {
        get {
            return _id;
        }
        set {
            _id = value;
        }
    }

    public string Username {
        get {
            return _username;
        }
        set {
            _username = value;
        }
    }

    public string FirstName {
        get {
            return _firstName;
        }
        set {
            _firstName = value;
        }
    }

    public string LastName {
        get {
            return _lastName;
        }
        set {
            _lastName = value;
        }
    }

    public Gender Gender {
        get {
            return _gender;
        }
        set {
            _gender = value;
        }
    }

}

public enum Gender {
    Male = 0,
    Female = 1,
    Other = 2
}
