using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class LaserShooter : ClickSpawner
{
    private bool isComboMode = false; // האם המצב הנוכחי הוא קומבו
    private enum Weapons { Laser, Curved ,Bomb }
    private Weapons currentWeapon = Weapons.Bomb; // מצב ברירת המחדל הוא לייזר רציף



    [SerializeField]
    [Tooltip("Input action for switching to Laser shooter weapon (default: 1)")]
    private InputAction laser = new InputAction(type: InputActionType.Button);

    [SerializeField]
    [Tooltip("Input action for switching to Bomb (default: 2)")]
    private InputAction bomb = new InputAction(type: InputActionType.Button);


    [SerializeField]
    [Tooltip("Input action for toggling combo mode (default: Enter)")]
    private InputAction toggleComboModeAction = new InputAction(type: InputActionType.Button);

    [SerializeField]
    [Tooltip("Input action for shooting (default: Space)")]
    private InputAction shootAction = new InputAction(type: InputActionType.Button);

    [SerializeField]
    [Tooltip("How many points to add to the shooter, if the laser hits its target")]
    int pointsToAdd = 1;

    [SerializeField, Range(1, 10)] // מאפשר לבחור ערך בטווח 1-10 ב-Inspector
    [Tooltip("Number of lasers to shoot in a combo (must be at least 1)")]
    int lasersInCombo = 3;

    [SerializeField]
    [Tooltip("Delay between lasers in a combo (seconds)")]
    float delayBetweenLasers = 0.2f;
   

    NumberField scoreField;

    private void OnEnable()
    {

        // הפעלת פעולות
        toggleComboModeAction.Enable();
        shootAction.Enable();
        laser.Enable();
        bomb.Enable();

        // קישור פעולות
        toggleComboModeAction.performed += ToggleComboMode; // ENTER משנה מצב
        shootAction.performed += PerformShoot; // SPACE יורה
        laser.performed += SwitchToLaser;  // 1 change to laser
        bomb.performed += SwitchToBomb; // 2 change to bomb
    }

    private void OnDisable()
    {
        // ביטול קישור פעולות
        toggleComboModeAction.Disable();
        shootAction.Disable();
        laser.Disable();
        bomb.Disable();

        toggleComboModeAction.performed -= ToggleComboMode;
        shootAction.performed -= PerformShoot;
        laser.performed -= SwitchToBomb;
        bomb.performed -= SwitchToLaser;
    }

    private void Start()
    {
        
        scoreField = GetComponentInChildren<NumberField>();
        if (!scoreField)
            Debug.LogError($"No child of {gameObject.name} has a NumberField component!");

        // הוספת Binding אם אין
        if (toggleComboModeAction.bindings.Count == 0)
            toggleComboModeAction.AddBinding("<Keyboard>/enter");
        if (laser.bindings.Count == 0)
            laser.AddBinding("<Keyboard>/1");
        if (bomb.bindings.Count == 0)
            bomb.AddBinding("<Keyboard>/2");
        if (shootAction.bindings.Count == 0)
            shootAction.AddBinding("<Keyboard>/space");
    }

    private void SwitchToLaser(InputAction.CallbackContext context)
    {
        currentWeapon = Weapons.Laser;
        Debug.Log("Switched to  Laser Weapon ");
    }

    private void SwitchToBomb(InputAction.CallbackContext context)
    {
        currentWeapon = Weapons.Bomb;
        Debug.Log("Switched to Bomb Weapon ");
    }

    private void ToggleComboMode(InputAction.CallbackContext context)
    {
        // שינוי מצב קומבו
        isComboMode = !isComboMode;
        Debug.Log("Combo Mode is now: " + (isComboMode ? "ON" : "OFF"));
    }

    private void PerformShoot(InputAction.CallbackContext context)
    {
        if (currentWeapon == Weapons.Laser)
        {
            // ירי לפי מצב קומבו
            if (isComboMode)
            {
                StartCoroutine(ShootCombo());
            }
            else
            {
                GameObject newObject = base.spawnObject();
                ScoreAdder newObjectScoreAdder = newObject.GetComponent<ScoreAdder>();
                if (newObjectScoreAdder)
                    newObjectScoreAdder.SetScoreField(scoreField).SetPointsToAdd(pointsToAdd);
            }
        }
        else if (currentWeapon == Weapons.Curved)
        { }
        else if (currentWeapon == Weapons.Bomb)
        {
            Vector2 throwDirection = transform.right;  // כיוון שבו השחקן פונה (לפי ה-X שלו)
            float throwForce = 10f;  // עוצמת הזריקה
            GameObject newObject = base.SpawnObjectAtPlayerPosition(throwDirection, throwForce);
            ScoreAdder newObjectScoreAdder = newObject.GetComponent<ScoreAdder>();
            if (newObjectScoreAdder)
                newObjectScoreAdder.SetScoreField(scoreField).SetPointsToAdd(pointsToAdd);
        }


    }


    private IEnumerator ShootCombo()
    {
        if (currentWeapon == Weapons.Laser)
        {
            for (int i = 0; i < lasersInCombo; i++)
            {
                GameObject newObject = base.spawnObject();

                ScoreAdder newObjectScoreAdder = newObject.GetComponent<ScoreAdder>();
                if (newObjectScoreAdder)
                    newObjectScoreAdder.SetScoreField(scoreField).SetPointsToAdd(pointsToAdd);

                yield return new WaitForSeconds(delayBetweenLasers);
            }
        }
        else if (currentWeapon == Weapons.Bomb)
        {
           
        }
    }


}

