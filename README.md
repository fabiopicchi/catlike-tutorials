# catlike-tutorials
Catlike coding Unity tutorials
http://catlikecoding.com/unity/tutorials/

Garbage collection tips:
- Don't use Invoke or StartCoroutine with a string.
- Don't use GUILayout and flag your GUI MonoBehaviour to prevent the 800bytes/frames of GUI garbage from occurring. (http://docs.unity3d.com/ScriptReference/MonoBehaviour-useGUILayout.html)
- Don't use GameObject.Tag or GameObject.Name
- Never use GetComponent on Update, and cache it if possible
- Don't use foreach
