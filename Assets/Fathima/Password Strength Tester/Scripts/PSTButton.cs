using System.Collections;
using System.Text; // Required for StringBuilder
using UnityEngine;
using TMPro; // Required for TextMeshPro

public class PSTButton : MonoBehaviour
{
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    public AudioClip buttonClickSound;
    private AudioSource audioSource;

    public string password;

    private TMP_Text buttonText; // Reference to the TextMeshPro text component

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        buttonText = GetComponentInChildren<TMP_Text>(); // Get the TMP_Text component on the button
        GeneratePassword(); // Generate a password when the game starts
    }

    public void GeneratePassword()
    {
        password = GenerateRandomPassword(8); // Generate an 8-character password
        buttonText.text = password; // Display the password on the button using TextMeshPro
    }

    private string GenerateRandomPassword(int length)
    {
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string special = "!@#$%^&*()_+-=[]{}|;:,.<>?";

        StringBuilder passwordBuilder = new StringBuilder();
        System.Random random = new System.Random();

        // Ensure the password contains at least one of each required type
        passwordBuilder.Append(upper[random.Next(upper.Length)]);
        passwordBuilder.Append(lower[random.Next(lower.Length)]);
        passwordBuilder.Append(digits[random.Next(digits.Length)]);
        passwordBuilder.Append(special[random.Next(special.Length)]);

        // Fill the remaining characters
        string allChars = upper + lower + digits + special;
        for (int i = 4; i < length; i++)
        {
            passwordBuilder.Append(allChars[random.Next(allChars.Length)]);
        }

        // Shuffle the password to ensure random order
        char[] passwordArray = passwordBuilder.ToString().ToCharArray();
        for (int i = passwordArray.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            char temp = passwordArray[i];
            passwordArray[i] = passwordArray[j];
            passwordArray[j] = temp;
        }

        return new string(passwordArray);
    }

    public void OnButtonClick()
    {
        audioSource.PlayOneShot(buttonClickSound);

        if (IsStrongPassword(password))
        {
            StartCoroutine(HandleCorrectPassword()); // Start a coroutine to handle correct password
        }
        else
        {
            audioSource.PlayOneShot(incorrectSound);
            FindObjectOfType<PSTGameManager>().WrongPassword();
        }
    }

    private IEnumerator HandleCorrectPassword()
    {
        Debug.Log("Correct Password: " + password);
        audioSource.PlayOneShot(correctSound);

        yield return new WaitForSeconds(correctSound.length); // Wait for the correct sound to finish playing

        FindObjectOfType<PSTGameManager>().CorrectPassword();
        gameObject.SetActive(false); // Disable button after sound plays
    }

    private bool IsStrongPassword(string password)
    {
        bool hasUpper = false, hasLower = false, hasDigit = false, hasSpecial = false;
        foreach (char c in password)
        {
            if (char.IsUpper(c)) hasUpper = true;
            else if (char.IsLower(c)) hasLower = true;
            else if (char.IsDigit(c)) hasDigit = true;
            else hasSpecial = true;

            if (hasUpper && hasLower && hasDigit && hasSpecial) return true;
        }
        return false;
    }
}
