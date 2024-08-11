using UnityEngine;
using UnityEngine.UI;

public class PSTButton : MonoBehaviour
{
    public Text passwordText;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    public AudioClip buttonClickSound;

    private AudioSource audioSource;
    private string password;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GeneratePassword();
    }

    public void OnButtonClick()
    {
        audioSource.PlayOneShot(buttonClickSound);

        if (IsStrongPassword(password))
        {
            audioSource.PlayOneShot(correctSound);
            FindObjectOfType<PSTGameManager>().CorrectPassword();
            gameObject.SetActive(false); // Disable button
        }
        else
        {
            audioSource.PlayOneShot(incorrectSound);
            FindObjectOfType<PSTGameManager>().WrongPassword();
        }
    }

    public void GeneratePassword()
    {
        password = GenerateRandomPassword();
        passwordText.text = password;
    }

    private string GenerateRandomPassword()
    {
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string symbols = "!@#$%^&*";

        string characters = upper + lower + digits + symbols;
        char[] passwordChars = new char[8];

        for (int i = 0; i < passwordChars.Length; i++)
        {
            passwordChars[i] = characters[Random.Range(0, characters.Length)];
        }

        return new string(passwordChars);
    }

    private bool IsStrongPassword(string password)
    {
        bool hasUpper = false, hasLower = false, hasDigit = false, hasSymbol = false;

        foreach (char c in password)
        {
            if (char.IsUpper(c)) hasUpper = true;
            else if (char.IsLower(c)) hasLower = true;
            else if (char.IsDigit(c)) hasDigit = true;
            else hasSymbol = true;
        }

        return hasUpper && hasLower && hasDigit && hasSymbol;
    }
}
