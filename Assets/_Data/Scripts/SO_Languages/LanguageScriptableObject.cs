using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "create language", menuName = "New Languages")]
public class LanguageScriptableObject : ScriptableObject
{
    [SerializeField] private GameObject[] Language;
}
