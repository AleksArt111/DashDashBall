using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;


public class Manager : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject[] _UiMenu;
    [SerializeField] private GameObject _spawn;
    [SerializeField] private GameObject[] _EnemySpawns;
    [SerializeField] private GameObject[] _EnemyType;
    [SerializeField] private GameObject[] _CoinSpawn;
    [SerializeField] private GameObject[] BuyorEquipUI;
    [SerializeField] private GameObject[] SkinsEquipOrBuy;
    [SerializeField] private GameObject[] EngLanguage;
    [SerializeField] private GameObject[] RusLanguage;
    [SerializeField] private GameObject _HightScoreInMenu;

    [SerializeField] private LanguageScriptableObject[] _languages;

    [SerializeField] private GameObject _adButton;

    private GameObject PlayerData;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _CoinPrefab;


    [SerializeField] private Slider _MusicSlider;


    [SerializeField] private GameObject GameplayMusic;
    [SerializeField] private GameObject MenuMusic;
    [SerializeField] private GameObject ErrorSound;

    private GameObject AlivePlayer;


    private PlayerScript _playerOnScene;

  

    [Header("Text")]
    [SerializeField] private Text _txt;
    [SerializeField] private Text _HighScore;
    [SerializeField] private Text[] _MoneyHaveInGame;
    [SerializeField] private Text[] _TextLiveWhenYouDie;

    [SerializeField] private Text _RecordRussianTranslate;

    [Header("Variables")]
    public int timeLived = 0;

    float timePast = 0;
    public float timeToChangeSpeed = 0;

    float MusicVolume = 0.084f;
    float SFXVolume;

    public int money;

    private int BuyedStyleBlue;
    private int BuyedStyleGreen;
    private int BuyedStyleWhite;
    private int BuyedStyleRed;



    private int EquipedStyleBlueOrDefault;
    private int EquipedStyleGreenOrDefault;
    private int EquipedStyleWhiteOrDefault;
    private int EquipedStyleRedOrDefault;
    private int EquipedStyleStandart;

    private int BuyedLightSkin;
    private int BuyedSnowManSkin;
    private int BuyedTechnoSkin;
    private int BuyedCosmoSkin;


    private int randomNumber;
    private int randomEnemy;
    private int _IsAppearedCoin;
    private int _randomSpawnCoin;

    public int IsFirstStartGame;

    private int Eng = 0;
    private int Rus = 0;

    private int _HightScorePoint;

    public float TimeRespawn = 5;

    public float WaitTimeRespawn;

    


    bool _isActive = false;
    bool _GameIsStart = false;
    bool _IsRespawn = false;
    bool _isRespawnFirstObject = true;
    bool IsCompleteDie = false;
    bool IsReklamaEnd = false;

    public bool PlayerNotWantToRevive = false;
    bool IsBought = false;
    bool IsEquiped = false;
    bool IsPressedSpawnForGuide = false;
    bool IsSpawnedWall = false;


    public bool PlayerWatchReklama;


    [Header("Image")]
    [SerializeField] private Image[] Buttons;

    [Header("Other")]
    private DeathPlayer deathPlayer;

    public IntertitialAds ad;

    public Shop shop;

    [SerializeField] private GameObject[] ColorBackGround;
    [SerializeField] private Color[] ColorForButtons;
    [SerializeField] private Image ButtonRecive;

    [SerializeField] private AudioSource _OpenGatesSFX;
    [SerializeField] private AudioSource _ClickSFX;
    [SerializeField] private AudioSource _CollectCoin;

 

    private int EquipedLightSkin;
    private int EquipedSnowManSkin;
    private int EquipedCosmoSkin;
    private int EquipedTechnoSkin;
    private int EquipedStandartSkin;


    private void OnEnable()
    {
        YandexGame.PromptShow();
    }

    private void Awake()
    {
        YandexGame.GetDataInvoke();
        YandexGame.PromptShow();
        _txt.text = Convert.ToString(timeLived);
        Convert.ToInt32(timeLived);
        timeLived = PlayerPrefs.GetInt("Time");
        _player.SetActive(true);
        money = PlayerPrefs.GetInt("Money");
        _MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");

        BuyedStyleBlue = PlayerPrefs.GetInt("BlueStyle");
        BuyedStyleGreen = PlayerPrefs.GetInt("GreenStyle");
        BuyedStyleWhite = PlayerPrefs.GetInt("WhiteStyle");
        BuyedStyleRed = PlayerPrefs.GetInt("RedStyle");

        Eng = PlayerPrefs.GetInt("EnglishChosen");
        Rus = PlayerPrefs.GetInt("RussianChosen");

        ErrorSound.SetActive(false);

        

        EquipedStyleBlueOrDefault = PlayerPrefs.GetInt("BlueStyleEquiped");
        EquipedStyleGreenOrDefault = PlayerPrefs.GetInt("GreenStyleEquiped");
        EquipedStyleWhiteOrDefault = PlayerPrefs.GetInt("WhiteStyleEquiped");
        EquipedStyleRedOrDefault = PlayerPrefs.GetInt("RedStyleEquiped");
        EquipedStyleStandart = PlayerPrefs.GetInt("StandartStyleEquiped");

        EquipedStandartSkin = PlayerPrefs.GetInt("StandartSkinEquiped");
        EquipedLightSkin = PlayerPrefs.GetInt("LightSkinEquiped");
        EquipedSnowManSkin = PlayerPrefs.GetInt("SnowManSkinEquiped");
        EquipedTechnoSkin = PlayerPrefs.GetInt("TechnoSkinEquiped");
        EquipedCosmoSkin = PlayerPrefs.GetInt("CosmoSkinEquiped");

        IsFirstStartGame = PlayerPrefs.GetInt("IsFirstStartGame");

        BuyedLightSkin = PlayerPrefs.GetInt("LightSkin");
        BuyedSnowManSkin = PlayerPrefs.GetInt("SnowManSkin");
        BuyedCosmoSkin = PlayerPrefs.GetInt("CosmoSkin");
        BuyedTechnoSkin = PlayerPrefs.GetInt("TechnoSkin");

        _HightScorePoint = PlayerPrefs.GetInt("HighScore");

        _HighScore.text = Convert.ToString(_HightScorePoint);


        StyleBlueProverka();
        StyleGreenProverka();
        StyleWhiteProverka();
        StyleRedProverka();
        StyleStandartProverka();

        StandartSkinProverka();
        LightSkinProverka();
        CosmoSkinProverka();
        TechnoSkin();
        SnowManSkinProverka();

        EnlishProverka();
        RussianProverka();

        RemoveAD(0);

        _languages[0] = EngLanguage;

        _HightScoreInMenu.GetComponent<Animator>().Play("HighScore");
        _HighScore.text = Convert.ToString(_HightScorePoint);
    }

    private void Start()
    {
        Application.targetFrameRate = 999;
    }

    private void Update()
    {
        _MoneyHaveInGame[1].text = money.ToString();
        PlayerPrefs.GetInt("Money");

        TextRecordMenu();
        StyleStandartProverka();
        StyleBlueProverka();
        StyleGreenProverka();
        StyleWhiteProverka();
        StyleRedProverka();

        CheckSkin();



        _HighScore.text = Convert.ToString(_HightScorePoint);


        if (_HightScorePoint < timePast)
        {
            _HighScore.text = Convert.ToString(_HightScorePoint);
            _HightScorePoint = timeLived;
            PlayerPrefs.SetInt("HighScore", _HightScorePoint);
        }

        ErrorSound.SetActive(true);

        AlivePlayer = GameObject.Find("Player(Clone)");
        GameStarted();
    }

    private void TrySaveHighScore()
    {
        YandexGame.NewLeaderboardScores(Convert.ToString(_HighScore), _HightScorePoint);
    }

    private void OnDisable()
    {
        YandexGame.SaveProgress();
    }
    void TextRecordMenu()
    {
        if(Rus == 1)
        {
            if (_HightScorePoint == 1)
            {
                _RecordRussianTranslate.text = "секунда";
            }
            if (_HightScorePoint < 5)
            {
                _RecordRussianTranslate.text = "секунды";
            }
            if (_HightScorePoint > 5)
            {
                _RecordRussianTranslate.text = "секунд";
            }
        }
    }
    public void AdCheck()
    {
        int _AdRemover = PlayerPrefs.GetInt("BuyedRemoveAD");
        bool IsGived = false;
        if (_AdRemover == 0)
        {
            if (ad.GetComponent<RewardedAds>()._AdIsShow == true)
            {
               PlayerPrefs.SetInt("Money", money += 500);
               ad.GetComponent<RewardedAds>()._AdIsShow = false;
            }
          }
        else
        {
            if(IsGived == false && _AdRemover == 1) 
            {
                ad.GetComponent<RewardedAds>()._AdIsShow = false;
                IsGived = true;     
            }

        }
    }

    void CheckSkin()
    {
        StandartSkinProverka();
        LightSkinProverka();
        SnowManSkinProverka();
        CosmoSkinProverka();
        TechnoSkinProverka();
    }

    void GameStarted()
    {
        if (_GameIsStart == true)
        {
            timeToChangeSpeed += Time.deltaTime;
            _txt.text = Convert.ToString(timeLived);
            _MoneyHaveInGame[0].text = money.ToString();
            _UiMenu[13].SetActive(true);
            MenuMusic.SetActive(false);
            timeLived = Convert.ToInt32(timePast);
            timePast += Time.deltaTime;

            if(Eng == 1)
            {
                _TextLiveWhenYouDie[0].text = Convert.ToString(timeLived);
            }
            else
            {
                _TextLiveWhenYouDie[1].text = Convert.ToString(timeLived);
            }

            if (_IsRespawn == false)
            {
                deathPlayer = FindAnyObjectByType<DeathPlayer>();
                randomNumber = UnityEngine.Random.Range(0, 2);
                randomEnemy = UnityEngine.Random.Range(0, 11);
                _IsAppearedCoin = UnityEngine.Random.Range(0, 11);
                _randomSpawnCoin = UnityEngine.Random.Range(0, 11);

                if (_IsAppearedCoin < 5)
                {
                    if(_randomSpawnCoin > 5)
                    {
                        Instantiate(_CoinPrefab, _CoinSpawn[0].transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(_CoinPrefab, _CoinSpawn[1].transform.position, Quaternion.identity);
                    }
                }

                if (randomEnemy <= 3)
                {
                    Instantiate(_EnemyType[0], _EnemySpawns[0].transform.position, Quaternion.identity);
                    _IsRespawn = true;
                    StartCoroutine("SpawnSecond");
                    if (_isRespawnFirstObject == false)
                    {
                        randomEnemy = UnityEngine.Random.Range(0, 11);
                        Invoke("SpawnSecondEnemyless3", WaitTimeRespawn);
                    }
                    StartCoroutine("RespawnBack");

                }
                else if (randomEnemy > 3 && randomEnemy < 6)
                {
                    Instantiate(_EnemyType[2], _EnemySpawns[0].transform.position, Quaternion.identity);
                    _IsRespawn = true;
                    StartCoroutine("SpawnSecond");
                    if (_isRespawnFirstObject == false)
                    {
                        randomEnemy = UnityEngine.Random.Range(0, 11);
                        Invoke("SpawnSecondEnemyplus3butnot6", WaitTimeRespawn);
                    }
                    StartCoroutine("RespawnBack");

                }
                else if (randomEnemy > 6)
                {
                    Instantiate(_EnemyType[4], _EnemySpawns[0].transform.position, Quaternion.identity);
                    _IsRespawn = true;
                    StartCoroutine("SpawnSecond");
                    if (_isRespawnFirstObject == false)
                    {
                        randomEnemy = UnityEngine.Random.Range(0, 11);
                        Invoke("SpawnSecondEnemy6plus", WaitTimeRespawn);
                    }

                    StartCoroutine("RespawnBack");

                }

            }
            if (_playerOnScene.IsDead == true && IsCompleteDie == false)
            {
                _UiMenu[13].SetActive(false);
                GameplayMusic.SetActive(false);
               
                StartCoroutine("GameOverMenu");
            }
        }
        else
        {
            MenuMusic.SetActive(true);
        }
    }

    void SpawnSecondEnemyless3()
    {
        if (randomEnemy > 6)
        {
            Instantiate(_EnemyType[5], _EnemySpawns[1].transform.position, Quaternion.identity);
        }

        else if (randomEnemy > 3 && randomEnemy < 6)
        {
            Instantiate(_EnemyType[3], _EnemySpawns[1].transform.position, Quaternion.identity);
        }

        else if (randomEnemy <= 3)
        {
            Instantiate(_EnemyType[1], _EnemySpawns[1].transform.position, Quaternion.identity);
        }
    }
    void SpawnSecondEnemyplus3butnot6()
    {
        if (randomEnemy > 6)
        {
            Instantiate(_EnemyType[5], _EnemySpawns[1].transform.position, Quaternion.identity);
        }
        else if (randomEnemy > 3 && randomEnemy < 6)
        {
            Instantiate(_EnemyType[3], _EnemySpawns[1].transform.position, Quaternion.identity);
        }
        else if (randomEnemy <= 3)
        {
            Instantiate(_EnemyType[1], _EnemySpawns[1].transform.position, Quaternion.identity); ;
        }
    }
    void SpawnSecondEnemy6plus()
    {
        if (randomEnemy > 6)
        {
            Instantiate(_EnemyType[5], _EnemySpawns[1].transform.position, Quaternion.identity);
        }
        else if (randomEnemy > 3 && randomEnemy < 6)
        {
            Instantiate(_EnemyType[3], _EnemySpawns[1].transform.position, Quaternion.identity);
        }
        else if (randomEnemy <= 3)
        {
            Instantiate(_EnemyType[1], _EnemySpawns[1].transform.position, Quaternion.identity);
        }
    }

    public void OnPressPlay()
    {
        MenuMusic.SetActive(false);
        MenuMusic.GetComponent<AudioSource>().Stop();
        _HightScoreInMenu.SetActive(false);
        if (_isActive == false)
        {
            _isActive = true;
            _UiMenu[0].GetComponent<Animator>().Play("PlayButtonClose");
            _UiMenu[1].GetComponent<Animator>().Play("NameTagClose");
            _UiMenu[2].GetComponent<Animator>().Play("ButtonInteractibleClose");
            _OpenGatesSFX.Play();
            _UiMenu[5].GetComponent<Animator>().Play("Open");   
            _player.SetActive(true);
            if (IsFirstStartGame == 0)
            {
                MenuMusic.GetComponent<AudioSource>().volume = 0;
                MenuMusic.SetActive(false);
                _UiMenu[10].SetActive(true);
                _UiMenu[11].SetActive(true);
                _HighScore.text = Convert.ToString(timeLived);
                _UiMenu[11].GetComponent<Animator>().Play("TapToContinue");
            }
            else
            {
                _GameIsStart = true;
                MenuMusic.SetActive(false);
                StartCoroutine("UIDissapered");
                Instantiate(_player, _spawn.transform.position, Quaternion.identity);
                _playerOnScene = FindAnyObjectByType<PlayerScript>();
            }
            GameplayMusic.SetActive(true);
            
            IsCompleteDie = false;
            timeLived = 0;
            timePast = 0;
        }
    }
    
    public void GuideForPlayer()
    {
        MenuMusic.SetActive(false);
        if (IsPressedSpawnForGuide == false)
        {
            MenuMusic.SetActive(false);
            MenuMusic.GetComponent<AudioSource>().Stop();
            IsPressedSpawnForGuide = true;
            Instantiate(_player, _spawn.transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("IsFirstStartGame", 1);
            IsFirstStartGame = 1;
            StartCoroutine("DisableUIBeginGuide");           
        }
        
    }
    public void ShopButtonInteractive()
    {
        _UiMenu[17].GetComponent<Animator>().Play("ClickShopButton");
        _UiMenu[0].GetComponent<Animator>().Play("PlayButtonClose");
        _UiMenu[1].GetComponent<Animator>().Play("NameTagClose");
        _UiMenu[2].GetComponent<Animator>().Play("ButtonInteractibleClose");
        _UiMenu[5].GetComponent<Animator>().Play("Open");
        StartCoroutine("ShopOpen");
    }
    public void BackFromShop()
    {
        _UiMenu[3].SetActive(false);
        _UiMenu[5].GetComponent<Animator>().Play("Close");
        StartCoroutine("ShopClose");
    }

    public void OptionsButtonPress()
    {
        _UiMenu[16].GetComponent<Animator>().Play("ClickOptionsButton");
        _UiMenu[0].GetComponent<Animator>().Play("PlayButtonClose");
        _UiMenu[1].GetComponent<Animator>().Play("NameTagClose");
        _UiMenu[2].GetComponent<Animator>().Play("ButtonInteractibleClose");
        _UiMenu[5].GetComponent<Animator>().Play("Open");
        StartCoroutine("OpenOptions");
    }

    public void BackFromOptions()
    {
        _UiMenu[9].SetActive(false);
        _UiMenu[5].GetComponent<Animator>().Play("Close");
        StartCoroutine("ShopClose");
    }

    public void SliderMusic()
    {
        MusicVolume = _MusicSlider.value;
        GameplayMusic.GetComponent<AudioSource>().volume = MusicVolume;
        MenuMusic.GetComponent<AudioSource>().volume = MusicVolume;
        //_Music[1].volume = MusicVolume;
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
    }

    public void RewardMoney()
    {
        money += 500;
    }

    public void ClickSoundEffect()
    {
        _ClickSFX.Play();
    }

    public void CollectCoins()
    {
        _CollectCoin.Play();
    }

    public void EngLanguageChosen()
    {
        Rus = 0;
        Eng = 1;
        EnlishProverka();
    }

    public void RusLanguageChosen()
    {
        Rus = 1;
        Eng = 0;
        RussianProverka(); 
    }

    void EnlishProverka()
    {
        if(Eng == 1)
        {
            Rus = 0;
            PlayerPrefs.SetInt("RussianChosen", 0);
            for (int i = 0; i < EngLanguage.Length; i++)
            {
                EngLanguage[i].SetActive(true);
                PlayerPrefs.SetInt("EnglishChosen", 1);
            }

            for (int i = 0; i < RusLanguage.Length; i++)
            {
                RusLanguage[i].SetActive(false);
            }
        }
    }
    void RussianProverka()
    {
        if (Rus == 1)
        {
            Eng = 0;
            PlayerPrefs.SetInt("EnglishChosen", 0);
            for (int i = 0; i < EngLanguage.Length; i++)
            {
                EngLanguage[i].SetActive(false);
                PlayerPrefs.SetInt("RussianChosen", 1);
            }
            for (int i = 0; i < RusLanguage.Length; i++)
            {
                RusLanguage[i].SetActive(true);
            }
        }
    }

    public  void RemoveAD(int BuyedAdRemover)
    {
        BuyedAdRemover = PlayerPrefs.GetInt("BuyedRemoveAD");
        if (BuyedAdRemover == 1)
            _adButton.SetActive(false);
    } 

    public void StyleBlueBuy()
    {
        if (BuyedStyleBlue == 0)
        {
            if (money >= 2000 && BuyedStyleBlue == 0)
            {
                BuyorEquipUI[0].SetActive(true);
                if (IsBought == true)
                {
                    PlayerPrefs.SetInt("Money", money -= 2000);
                    PlayerPrefs.SetInt("BlueStyle", 1);
                    IsBought = false;
                }
            }
            else
            {
                ErrorSound.GetComponent<AudioSource>().Play();
            }
            
        }
        if (BuyedStyleBlue == 1)
        {
            BuyorEquipUI[1].SetActive(true);
            if (IsEquiped == true)
            {
                EquipedStyleBlueOrDefault = 1;
                PlayerPrefs.SetInt("BlueStyleEquiped", 1);
                StyleBlueProverka();
                IsEquiped = false;
            }
        }
    }

    public void StyleGreenBuy()
    {
        if (BuyedStyleGreen == 0)
        {
            if (money >= 2000 && BuyedStyleGreen == 0)
            {
                BuyorEquipUI[0].SetActive(true);
                if (IsBought == true)
                {
                    PlayerPrefs.SetInt("Money", money -= 2000);
                    BuyedStyleGreen = 1;
                    PlayerPrefs.SetInt("GreenStyle", 1);
                    IsBought = false;
                }
            }
            else
            {
                ErrorSound.GetComponent<AudioSource>().Play();
            }

        }
        if (BuyedStyleGreen == 1)
        {
            BuyorEquipUI[1].SetActive(true);
            if (IsEquiped == true)
            {
                EquipedStyleGreenOrDefault = 1;
                PlayerPrefs.SetInt("GreenStyleEquiped", 1);
                StyleGreenProverka();
                IsEquiped = false;
            }
        }
    }

    public void StyleWhiteBuy()
    {
        if (BuyedStyleWhite == 0)
        {
            if (money >= 2000 && BuyedStyleWhite == 0)
            {
                BuyorEquipUI[0].SetActive(true);
                if (IsBought == true)
                {
                    PlayerPrefs.SetInt("Money", money -= 2000);
                    BuyedStyleWhite = 1;
                    PlayerPrefs.SetInt("WhiteStyle", 1);
                    IsBought = false;
                }
            }
            else
            {
                ErrorSound.GetComponent<AudioSource>().Play();
            }

        }
        if (BuyedStyleWhite == 1)
        {
            BuyorEquipUI[1].SetActive(true);
            if (IsEquiped == true)
            {
                EquipedStyleWhiteOrDefault = 1;
                PlayerPrefs.SetInt("WhiteStyleEquiped", 1);
                StyleWhiteProverka();
                IsEquiped = false;
            }
        }
    }

    public void StyleRedBuy()
    {
        if (BuyedStyleRed == 0)
        {
            if (money >= 2000)
            {
                BuyorEquipUI[0].SetActive(true);
                if (IsBought == true && BuyedStyleRed == 0)
                {
                    PlayerPrefs.SetInt("Money", money -= 10000);
                    BuyedStyleRed = 1;
                    PlayerPrefs.SetInt("RedStyle", 1);
                    IsBought = false;
                }
            }
            else
            {
                ErrorSound.GetComponent<AudioSource>().Play();
            }

        }
        if (BuyedStyleRed == 1)
        {
            BuyorEquipUI[1].SetActive(true);
            if (IsEquiped == true)
            {
                EquipedStyleRedOrDefault = 1;
                PlayerPrefs.SetInt("RedStyleEquiped", 1);
                StyleRedProverka();
                IsEquiped = false;
            }
        }
    }

    public void StandartStyle()
    {
        BuyorEquipUI[1].SetActive(true);
        if (IsEquiped == true)
        {
            EquipedStyleStandart = 1;
            PlayerPrefs.SetInt("StandartStyleEquiped", 1);
            StyleStandartProverka();
            IsEquiped = false;
        }
    }

    public void BuyStyle()
    {
        IsBought = true;
        BuyorEquipUI[0].SetActive(false);
    }

    public void EquipStyle()
    {
        IsEquiped = true;
        BuyorEquipUI[1].SetActive(false);
    }

    public void BuySkin()
    {
        IsBought = true;
        SkinsEquipOrBuy[0].SetActive(false);
    }

    public void EquipSkin()
    {
        IsEquiped = true;
        SkinsEquipOrBuy[1].SetActive(false);
    }

    public void NotAgreeButton()
    {
        BuyorEquipUI[1].SetActive(false);
        SkinsEquipOrBuy[1].SetActive(false);
    }

    void StyleBlueProverka()
    {
        if (EquipedStyleBlueOrDefault == 1)
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].color = ColorForButtons[1];
            }
            for (int i = 0; i < 4; i++)
            {
                Buttons[i].color = ColorForButtons[1];
                ColorBackGround[1].SetActive(true);
                ColorBackGround[i].SetActive(false);
            }
            
            EquipedStyleGreenOrDefault = 0;
            EquipedStyleRedOrDefault = 0;
            EquipedStyleWhiteOrDefault = 0;
            EquipedStyleStandart = 0;
        }
    }

    void StyleGreenProverka()
    {
        if (EquipedStyleGreenOrDefault == 1)
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].color = ColorForButtons[2];
            }
            for (int i = 0; i < 4; i++)
            {
                ColorBackGround[i].SetActive(false);
                ColorBackGround[2].SetActive(true);
            }
            
            EquipedStyleWhiteOrDefault = 0;
            EquipedStyleBlueOrDefault = 0;
            EquipedStyleRedOrDefault = 0;
            EquipedStyleStandart = 0;
        }
    }

    void StyleWhiteProverka()
    {
        if (EquipedStyleWhiteOrDefault == 1)
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].color = ColorForButtons[0];
            }
            for (int i = 0; i < 4; i++)
            {
                ColorBackGround[3].SetActive(true);
                ColorBackGround[i].SetActive(false);
            }
            
            EquipedStyleRedOrDefault = 0;
            EquipedStyleBlueOrDefault = 0;
            EquipedStyleGreenOrDefault = 0;
            EquipedStyleStandart = 0;
        }
    }

    void StyleRedProverka()
    {
        if (EquipedStyleRedOrDefault == 1)
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].color = ColorForButtons[0];
            }
         
            for (int i = 0; i < 4; i++)
            {
                ColorBackGround[i].SetActive(false);
                ColorBackGround[4].SetActive(true);
            }

            EquipedStyleWhiteOrDefault = 0;
            EquipedStyleBlueOrDefault = 0;
            EquipedStyleGreenOrDefault = 0;
            EquipedStyleStandart = 0;
        }
    }

    void StyleStandartProverka()
    {
        if(EquipedStyleStandart == 1)
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].color = ColorForButtons[0];
            }
            for (int i = 0; i < 4; i++)
            {
                ColorBackGround[0].SetActive(true);
                ColorBackGround[i].SetActive(false);
            }
            EquipedStyleWhiteOrDefault = 0;
            EquipedStyleBlueOrDefault = 0;
            EquipedStyleRedOrDefault = 0;
            EquipedStyleGreenOrDefault = 0;
        }
    }

    public void StandartSkinEqupied()
    {
        SkinsEquipOrBuy[1].SetActive(true);
        if (IsEquiped == true)
        {
            EquipedLightSkin = 0;
            PlayerPrefs.SetInt("LightSkinEquiped", 0);
            EquipedCosmoSkin = 0;
            PlayerPrefs.SetInt("CosmoSkinEquiped", 0);
            EquipedSnowManSkin = 0;
            PlayerPrefs.SetInt("SnowManSkinEquiped", 0);
            EquipedStandartSkin = 1;
            PlayerPrefs.SetInt("StandartSkinEquiped", 1);
            EquipedTechnoSkin = 0;
            PlayerPrefs.SetInt("TechnoSkinEquiped", 0);
            StandartSkinProverka();
            IsEquiped = false;
        }    
    }

    public void LightSkin()
    {
        if (BuyedLightSkin == 0)
        {
            if (money >= 2500 && BuyedLightSkin == 0)
            {
                SkinsEquipOrBuy[0].SetActive(true);
                if (IsBought == true)
                {
                    PlayerPrefs.SetInt("Money", money -= 2500);
                    BuyedLightSkin = 1;
                    PlayerPrefs.SetInt("LightSkin", 1);
                    IsBought = false;
                }
            }
            else
            {
                ErrorSound.GetComponent<AudioSource>().Play();
            }

        }
        if (BuyedLightSkin == 1)
        {
            SkinsEquipOrBuy[1].SetActive(true);
            if (IsEquiped == true)
            {
                EquipedLightSkin = 1;
                PlayerPrefs.SetInt("LightSkinEquiped", 1);
                EquipedCosmoSkin = 0;
                PlayerPrefs.SetInt("CosmoSkinEquiped", 0);
                EquipedSnowManSkin = 0;
                PlayerPrefs.SetInt("SnowManSkinEquiped", 0);
                EquipedStandartSkin = 0;
                PlayerPrefs.SetInt("StandartSkinEquiped", 0);
                EquipedTechnoSkin = 0;
                PlayerPrefs.SetInt("TechnoSkinEquiped", 0);
                LightSkinProverka();
                IsEquiped = false;
            }
        }
    }

    public void SnowManSkin()
    {
        if (BuyedSnowManSkin == 0)
        {
            if (money >= 1500 && BuyedSnowManSkin == 0)
            {
                SkinsEquipOrBuy[0].SetActive(true);
                if (IsBought == true)
                {
                    PlayerPrefs.SetInt("Money", money -= 1500);
                    BuyedSnowManSkin = 1;
                    PlayerPrefs.SetInt("SnowManSkin", 1);
                    IsBought = false;
                }
            }
            else
            {
                ErrorSound.GetComponent<AudioSource>().Play();
            }

        }
        if (BuyedSnowManSkin == 1)
        {
            SkinsEquipOrBuy[1].SetActive(true);
            if (IsEquiped == true)
            {
                EquipedLightSkin = 0;
                PlayerPrefs.SetInt("LightSkinEquiped", 0);
                EquipedCosmoSkin = 0;
                PlayerPrefs.SetInt("CosmoSkinEquiped", 0);
                EquipedSnowManSkin = 1;
                PlayerPrefs.SetInt("SnowManSkinEquiped", 1);
                EquipedStandartSkin = 0;
                PlayerPrefs.SetInt("StandartSkinEquiped", 0);
                EquipedTechnoSkin = 0;
                PlayerPrefs.SetInt("TechnoSkinEquiped", 0);
                SnowManSkinProverka();
                IsEquiped = false;
            }
        }
    }

    public void CosmoSkin()
    {
        if (BuyedCosmoSkin == 0)
        {
            if (money >= 1500 && BuyedCosmoSkin == 0)
            {
                SkinsEquipOrBuy[0].SetActive(true);
                if (IsBought == true)
                {
                    PlayerPrefs.SetInt("Money", money -= 1500);
                    BuyedCosmoSkin = 1;
                    PlayerPrefs.SetInt("CosmoSkin", 1);
                    IsBought = false;
                }
            }
            else
            {
                ErrorSound.GetComponent<AudioSource>().Play();
            }

        }
        if (BuyedCosmoSkin == 1)
        {
            SkinsEquipOrBuy[1].SetActive(true);
            if (IsEquiped == true)
            {
                EquipedLightSkin = 0;
                PlayerPrefs.SetInt("LightSkinEquiped", 0);
                EquipedCosmoSkin = 1;
                PlayerPrefs.SetInt("CosmoSkinEquiped", 1);
                EquipedSnowManSkin = 0;
                PlayerPrefs.SetInt("SnowManSkinEquiped", 0);
                EquipedStandartSkin = 0;
                PlayerPrefs.SetInt("StandartSkinEquiped", 0);
                EquipedTechnoSkin = 0;
                PlayerPrefs.SetInt("TechnoSkinEquiped", 0);
                CosmoSkinProverka();
                IsEquiped = false;
            }
        }
    }

    public void TechnoSkin()
    {
        if (BuyedTechnoSkin == 0)
        {
            if (money >= 2500 && BuyedTechnoSkin == 0)
            {
                SkinsEquipOrBuy[0].SetActive(true);
                if (IsBought == true)
                {
                    PlayerPrefs.SetInt("Money", money -= 2500);
                    BuyedTechnoSkin = 1;
                    PlayerPrefs.SetInt("TechnoSkin", 1);
                    IsBought = false;
                }
            }
            else
            {
                ErrorSound.GetComponent<AudioSource>().Play();
            }

        }
        if (BuyedTechnoSkin == 1)
        {
            SkinsEquipOrBuy[1].SetActive(true);
            if (IsEquiped == true)
            {
                EquipedLightSkin = 0;
                PlayerPrefs.SetInt("LightSkinEquiped", 0);
                EquipedCosmoSkin = 0;
                PlayerPrefs.SetInt("CosmoSkinEquiped", 0);
                EquipedSnowManSkin = 0;
                PlayerPrefs.SetInt("SnowManSkinEquiped", 0);
                EquipedStandartSkin = 0;
                PlayerPrefs.SetInt("StandartSkinEquiped", 0);
                EquipedTechnoSkin = 1;
                PlayerPrefs.SetInt("TechnoSkinEquiped", 1);
                TechnoSkinProverka();
                IsEquiped = false;
            }
        }
    }


    private void LightSkinProverka()
    {
        if(EquipedLightSkin == 1)
        {
            _player.GetComponent<PlayerScript>().Skins[1].SetActive(true);
            _player.GetComponent<PlayerScript>().Skins[0].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[2].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[3].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[4].SetActive(false);
        }
    }
    private void SnowManSkinProverka()
    {
        if(EquipedSnowManSkin == 1)
        {
            _player.GetComponent<PlayerScript>().Skins[0].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[1].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[2].SetActive(true);
            _player.GetComponent<PlayerScript>().Skins[3].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[4].SetActive(false);
        }
    }

    private void CosmoSkinProverka()
    {
        if(EquipedCosmoSkin == 1)
        {
           
            _player.GetComponent<PlayerScript>().Skins[0].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[1].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[2].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[3].SetActive(true);
            _player.GetComponent<PlayerScript>().Skins[4].SetActive(false);
        }
    }
    private void TechnoSkinProverka()
    {
        if (EquipedTechnoSkin == 1)
        {
            _player.GetComponent<PlayerScript>().Skins[0].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[1].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[2].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[3].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[4].SetActive(true);
        }
    }

    private void StandartSkinProverka()
    {
        if (EquipedStandartSkin == 1) 
        {
            _player.GetComponent<PlayerScript>().Skins[0].SetActive(true);
            _player.GetComponent<PlayerScript>().Skins[1].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[2].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[3].SetActive(false);
            _player.GetComponent<PlayerScript>().Skins[4].SetActive(false);
        }
    }

    IEnumerator UIAppeared()
    {
        yield return new WaitForSeconds(1);

        for(int i = 0; i < 4; i++)
        {
            _UiMenu[i].SetActive(true);
            _UiMenu[4].SetActive(false);
        }  
        _isActive = false;
        _GameIsStart = false;   
    }

    IEnumerator UIDissapered()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 5; i++)
        {
            _UiMenu[i].SetActive(false);
            _UiMenu[4].SetActive(true);
        }
        _isActive = false;  
    }

    IEnumerator TimeLived()
    {
        yield return new WaitForSecondsRealtime(1);
        if(Eng == 1)
        {
            _UiMenu[8].GetComponent<Animator>().Play("LiveRecordf");
            _UiMenu[12].SetActive(false);
        }
        else
        {
            _UiMenu[19].GetComponent<Animator>().Play("LiveRecordf");
            _UiMenu[12].SetActive(false);
        }
        TimeRespawn = 3;
        StartCoroutine("CloseRewarUI");
    }
    IEnumerator CloseRewarUI()
    {
        yield return new WaitForSecondsRealtime(3);

        _UiMenu[6].SetActive(false);
        StartCoroutine("ShopClose");
    }
    IEnumerator RespawnBack()
    {
        yield return new WaitForSecondsRealtime(2f);
        _IsRespawn = false;
    }

    IEnumerator SpawnSecond()
    {
        yield return new WaitForSecondsRealtime(WaitTimeRespawn);
        _isRespawnFirstObject = false;
    }

    IEnumerator ShopOpen()
    {
        yield return new WaitForSecondsRealtime(1f);
        _UiMenu[0].SetActive(false);
        _UiMenu[1].SetActive(false);
        _UiMenu[2].SetActive(false);
        _UiMenu[14].SetActive(true);
        _UiMenu[3].SetActive(true);
        BuyorEquipUI[1].SetActive(false);
        SkinsEquipOrBuy[1].SetActive(false);
    }
    IEnumerator ShopClose()
    {
        yield return new WaitForSecondsRealtime(1f);
        _UiMenu[0].SetActive(true);
        _UiMenu[1].SetActive(true);
        _UiMenu[2].SetActive(true);
        _UiMenu[0].GetComponent<Animator>().Play("PlayButtonOpen");
        _UiMenu[1].GetComponent<Animator>().Play("NameTagOpen");
        _UiMenu[2].GetComponent<Animator>().Play("ButtonInteractibleOpen");
        _HightScoreInMenu.SetActive(true);
    }
    IEnumerator OpenOptions()
    {
        yield return new WaitForSecondsRealtime(1);
        _UiMenu[0].SetActive(false);
        _UiMenu[1].SetActive(false);
        _UiMenu[2].SetActive(false);
        _UiMenu[9].SetActive(true);
        _HightScoreInMenu.SetActive(false);
    }
    IEnumerator DisableUIBeginGuide()
    {
        yield return new WaitForSecondsRealtime(1);
        _UiMenu[10].SetActive(false);
        _UiMenu[11].SetActive(false);
        MenuMusic.SetActive(false);
        StartCoroutine("UIDissapered");
        _playerOnScene = FindAnyObjectByType<PlayerScript>();
        _GameIsStart = true;
        MenuMusic.GetComponent<AudioSource>().volume = 0.084f;
    }
    IEnumerator GameOverMenu()
    {
        yield return new WaitForSecondsRealtime(0);

        WatchReklama();
    
    }

    public void WatchReklama()
    {
        PlayerNotWantToRevive = false;
        int i = UnityEngine.Random.Range(0, 11);
        Debug.Log(i);
        if (i <= 5)
        {
            //ad.ShowAd();
            YandexGame.FullscreenShow();

        }
        GameplayMusic.SetActive(false);
        MenuMusic.GetComponent<AudioSource>().Play();
        IsCompleteDie = true;
        _GameIsStart = false;
        _UiMenu[4].SetActive(false);
        _UiMenu[5].GetComponent<Animator>().Play("Close");
        _UiMenu[6].SetActive(true);
        if(Eng == 1)
        {
            _UiMenu[7].GetComponent<Animator>().Play("DieText");
        }
        else
        {
            _UiMenu[18].GetComponent<Animator>().Play("DieText");
        }
        StartCoroutine("TimeLived");
    }

    [System.Serializable]

    public class Shop
    {
        [SerializeField] private GameObject Day;
        public string mamatvoya;

    }

    
}
