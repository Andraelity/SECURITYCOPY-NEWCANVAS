using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;

using readDataFromUI;
using readDataFile;


public class C0CS_WordGane_V_0_0_9 : MonoBehaviour
{


    private int HEIGHT = 1000;
    private int WIDTH  = 2000;
    
    private int HEIGHTlargeMemory = 3000;
    private int WIDTHlargeMemory  = 6000;
    

    private float time;

    public float timeNow;
    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames;
    private float fps;
    
    public const int CHARACTERWIDTH = 34;
    public const int CHARACTERWIDTH_size14 = 12;
    public const int CHARACTERWIDTH_size22 = 17;

    private float changeInterval;


    public GameObject object_PlaneWriteTextField;

    Renderer render;
    Material material_Write;
    Material material_PlaneWriteTextField;

    public ComputeShader computacion;


    ComputeBuffer bufferImageColor;
    ComputeBuffer bufferImageColorWriteTextField;
    ComputeBuffer bufferLargeMemory;

    ComputeBuffer bufferTextureTransparentPositionX;
    ComputeBuffer bufferTextureTransparentPositionY;

    ComputeBuffer bufferTextureBigPositionX;
    ComputeBuffer bufferTextureBigPositionY;

    ComputeBuffer bufferTextureCharacterPositionX;
    ComputeBuffer bufferTextureCharacterPositionY;

    ComputeBuffer bufferTextureAlphabetColor;
    ComputeBuffer bufferTextureNumberColor;
    ComputeBuffer bufferTexturePortugueseColor;



    ComputeBuffer bufferTextureCharacterPositionX_size14;
    ComputeBuffer bufferTextureCharacterPositionY_size14;
    
    ComputeBuffer bufferTextureAlphabetColor_size14;
    ComputeBuffer bufferTextureNumberColor_size14;
    ComputeBuffer bufferTexturePortugueseColor_size14;


    ComputeBuffer bufferTextureCharacterPositionX_size22;
    ComputeBuffer bufferTextureCharacterPositionY_size22;
    
    ComputeBuffer bufferTextureAlphabetColor_size22;
    ComputeBuffer bufferTextureNumberColor_size22;
    ComputeBuffer bufferTexturePortugueseColor_size22;

    ComputeBuffer bufferPointerPositionX;
    ComputeBuffer bufferPointerPositionY;


    ComputeBuffer bufferColorPointer;

    ComputeBuffer bufferHorizontalPositionX;
    ComputeBuffer bufferHorizontalPositionY;

    ComputeBuffer bufferVerticalPositionX;
    ComputeBuffer bufferVerticalPositionY;




    static int _kernelforLoadImageColor;
    static int _kernelforLoadImageColorWriteTextField;

    static int _kernelforLoadTextureBig;
    static int _kernelforPrintTextureBig;

    static int _kernelforLoadTextureTransparent;
    static int _kernelforPrintTextureTransparent;


    static int _kernelforLoadTextureCharacterPosition;

    static int _kernelforPrintTilesAlphabetWriteTextField;

    static int _kernelforPrintTilesAlphabetIndividualWriteTextField;
    static int _kernelforPrintTilesNumberIndividualWriteTextField;
    static int _kernelforPrintTilesPortugueseIndividualWriteTextField;

    static int _kernelforPrintTilesAlphabet;

    static int _kernelforPrintTilesAlphabetIndividual;
    static int _kernelforPrintTilesNumberIndividual;
    static int _kernelforPrintTilesPortugueseIndividual;

    
    static int _kernelforLoadTextureCharacterPosition_size14;

    static int _kernelforPrintTilesAlphabet_size14;

    static int _kernelforPrintTilesAlphabetIndividual_size14;
    static int _kernelforPrintTilesNumberIndividual_size14;
    static int _kernelforPrintTilesPortugueseIndividual_size14;


    static int _kernelforLoadTextureCharacterPosition_size22;

    static int _kernelforPrintTilesAlphabet_size22;

    static int _kernelforPrintTilesAlphabetIndividual_size22;
    static int _kernelforPrintTilesNumberIndividual_size22;
    static int _kernelforPrintTilesPortugueseIndividual_size22;


    static int _kernelforLoadPointerPosition;

    static int _kernelforPrintPointer;

    static int _kernelforPrintPointerBlack;


    static int _kernelforLoadHorizontalPosition;
    static int _kernelforPrintHorizontal;
    static int _kernelforPrintHorizontalBlack;


    static int _kernelforLoadVerticalPosition;
    static int _kernelforPrintVertical;
    static int _kernelforPrintVerticalBlack;



    int receiveSize = 0;
    int manageSize = 0;

    const int constPositionPointerX = 1949;
    const int constPositionPointerY = 945;

    int PositionPointerX = 1949;
    int PositionPointerY = 945;

    bool activo = false;
    bool activo2 = false;


    static string StringTextField = "";

    int countPositionCharacter = 0;

    bool setFlagFind = false;


    int _XaxisWord22 = 1900;
    int _YaxisWord22 = 500;


    const int _XaxisTextFieldBegin = 1940;
    const int _YaxisTextFieldBegin = 935;

    int _XaxisTextField = _XaxisTextFieldBegin;
    int _YaxisTextField = _YaxisTextFieldBegin;

    bool shiftKey = false;

    bool afterAppostrophe = false;
    int appostrophe = 0;
    

    bool wordCheck0 = false;
    bool CheckBool = true;
    bool CheckBool1 = false;

    

    void OnGUI()
    {

        GUILayout.Label("" + fps.ToString("f2"));

    }
    
    // Start is called before the first frame update

    void Start()
    {

        
        render = GetComponent<Renderer>();
        material_Write = render.material;
        material_PlaneWriteTextField = object_PlaneWriteTextField.GetComponent<Renderer>().material; 

    
        computacion.SetInt("_WIDTH", WIDTH);
        computacion.SetInt("_WIDTHlargeMemory", WIDTHlargeMemory);



        material_PlaneWriteTextField.SetInt("_WIDTH", WIDTH);
        material_PlaneWriteTextField.SetInt("_HEIGHT", HEIGHT);

        material_Write.SetInt("_WIDTH", WIDTH);
        material_Write.SetInt("_HEIGHT", HEIGHT);

     
        bufferLargeMemory = new ComputeBuffer(HEIGHTlargeMemory * WIDTHlargeMemory, 16);

        bufferImageColor = new ComputeBuffer(HEIGHT * WIDTH, 16);

        bufferImageColorWriteTextField = new ComputeBuffer(HEIGHT * WIDTH, 16);


        material_Write.SetBuffer("bufferImageColor", bufferImageColor);
        material_PlaneWriteTextField.SetBuffer("bufferImageColorWriteTextField", bufferImageColorWriteTextField);


        _kernelforLoadImageColor = computacion.FindKernel("forLoadImageColor");


        _kernelforLoadTextureTransparent = computacion.FindKernel("forLoadTextureTransparent");
        _kernelforPrintTextureTransparent = computacion.FindKernel("forPrintTextureTransparent");

        _kernelforLoadTextureBig = computacion.FindKernel("forLoadTextureBig");
        _kernelforPrintTextureBig = computacion.FindKernel("forPrintTextureBig");


        _kernelforLoadTextureCharacterPosition = computacion.FindKernel("forLoadTextureCharacterPosition");


        _kernelforPrintTilesAlphabetWriteTextField = computacion.FindKernel("forPrintTilesAlphabetWriteTextField");
 
        _kernelforPrintTilesAlphabetIndividualWriteTextField = computacion.FindKernel("forPrintTilesAlphabetIndividualWriteTextField");
        _kernelforPrintTilesNumberIndividualWriteTextField = computacion.FindKernel("forPrintTilesNumberIndividualWriteTextField");
        _kernelforPrintTilesPortugueseIndividualWriteTextField = computacion.FindKernel("forPrintTilesPortugueseIndividualWriteTextField");


        _kernelforPrintTilesAlphabet = computacion.FindKernel("forPrintTilesAlphabet");

        _kernelforPrintTilesAlphabetIndividual = computacion.FindKernel("forPrintTilesAlphabetIndividual");
        _kernelforPrintTilesNumberIndividual = computacion.FindKernel("forPrintTilesNumberIndividual");
        _kernelforPrintTilesPortugueseIndividual = computacion.FindKernel("forPrintTilesPortugueseIndividual");



        _kernelforLoadTextureCharacterPosition_size14  = computacion.FindKernel("forLoadTextureCharacterPosition_size14");

        _kernelforPrintTilesAlphabet_size14 = computacion.FindKernel("forPrintTilesAlphabet_size14");

        _kernelforPrintTilesAlphabetIndividual_size14 = computacion.FindKernel("forPrintTilesAlphabetIndividual_size14");
        _kernelforPrintTilesNumberIndividual_size14 = computacion.FindKernel("forPrintTilesNumberIndividual_size14");
        _kernelforPrintTilesPortugueseIndividual_size14 = computacion.FindKernel("forPrintTilesPortugueseIndividual_size14");



        _kernelforLoadTextureCharacterPosition_size22  = computacion.FindKernel("forLoadTextureCharacterPosition_size22");

        _kernelforPrintTilesAlphabet_size22 = computacion.FindKernel("forPrintTilesAlphabet_size22");

        _kernelforPrintTilesAlphabetIndividual_size22 = computacion.FindKernel("forPrintTilesAlphabetIndividual_size22");
        _kernelforPrintTilesNumberIndividual_size22 = computacion.FindKernel("forPrintTilesNumberIndividual_size22");
        _kernelforPrintTilesPortugueseIndividual_size22 = computacion.FindKernel("forPrintTilesPortugueseIndividual_size22");



        _kernelforLoadPointerPosition = computacion.FindKernel("forLoadPointerPosition");

        _kernelforPrintPointer = computacion.FindKernel("forPrintPointer");
        _kernelforPrintPointerBlack = computacion.FindKernel("forPrintPointerBlack");


        _kernelforLoadHorizontalPosition = computacion.FindKernel("forLoadHorizontalPosition");
        _kernelforPrintHorizontal = computacion.FindKernel("forPrintHorizontal");
        _kernelforPrintHorizontalBlack = computacion.FindKernel("forPrintHorizontalBlack");

        _kernelforLoadVerticalPosition = computacion.FindKernel("forLoadVerticalPosition");
        _kernelforPrintVertical = computacion.FindKernel("forPrintVertical");
        _kernelforPrintVerticalBlack = computacion.FindKernel("forPrintVerticalBlack");





        bufferTextureTransparentPositionX = new ComputeBuffer(1000000, 4);
        bufferTextureTransparentPositionY = new ComputeBuffer(1000000, 4);

        bufferTextureBigPositionX = new ComputeBuffer(HEIGHT * WIDTH, 4);
        bufferTextureBigPositionY = new ComputeBuffer(HEIGHT * WIDTH, 4);


        bufferTextureCharacterPositionX = new ComputeBuffer(43904, 4);
        bufferTextureCharacterPositionY = new ComputeBuffer(43904, 4);

        bufferTextureAlphabetColor = new ComputeBuffer(175616, 4); 
        
        bufferTextureNumberColor = new ComputeBuffer(175616, 4); 

        bufferTexturePortugueseColor = new ComputeBuffer(175616, 4); 


        bufferTextureCharacterPositionX_size14 = new ComputeBuffer(6468, 4);
        bufferTextureCharacterPositionY_size14 = new ComputeBuffer(6468, 4);

        bufferTextureAlphabetColor_size14 = new ComputeBuffer(25872, 4); 
        
        bufferTextureNumberColor_size14 = new ComputeBuffer(25872, 4); 

        bufferTexturePortugueseColor_size14 = new ComputeBuffer(25872, 4); 



        bufferTextureCharacterPositionX_size22 = new ComputeBuffer(16184, 4);
        bufferTextureCharacterPositionY_size22 = new ComputeBuffer(16184, 4);

        bufferTextureAlphabetColor_size22 = new ComputeBuffer(64736, 4); 
        
        bufferTextureNumberColor_size22 = new ComputeBuffer(64736, 4); 

        bufferTexturePortugueseColor_size22 = new ComputeBuffer(64736, 4); 


        bufferPointerPositionX = new ComputeBuffer(440, 4);
        bufferPointerPositionY = new ComputeBuffer(440, 4);

        bufferColorPointer = new ComputeBuffer(3, 4);

        
        bufferHorizontalPositionX = new ComputeBuffer(280, 4);
        bufferHorizontalPositionY = new ComputeBuffer(280, 4);

        bufferVerticalPositionX = new ComputeBuffer(280, 4);
        bufferVerticalPositionY = new ComputeBuffer(280, 4);



        computacion.SetBuffer(_kernelforLoadImageColor, "bufferImageColor", bufferImageColor);
        computacion.SetBuffer(_kernelforLoadImageColor, "bufferLargeMemory", bufferLargeMemory);

        computacion.SetBuffer(_kernelforLoadTextureBig, "bufferTextureBigPositionX",bufferTextureBigPositionX);
        computacion.SetBuffer(_kernelforLoadTextureBig, "bufferTextureBigPositionY",bufferTextureBigPositionY);

        computacion.SetBuffer(_kernelforPrintTextureBig,"bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTextureBig,"bufferTextureBigPositionX",bufferTextureBigPositionX);
        computacion.SetBuffer(_kernelforPrintTextureBig,"bufferTextureBigPositionY",bufferTextureBigPositionY);

        computacion.SetBuffer(_kernelforLoadTextureTransparent,"bufferTextureTransparentPositionX", bufferTextureTransparentPositionX);
        computacion.SetBuffer(_kernelforLoadTextureTransparent,"bufferTextureTransparentPositionY", bufferTextureTransparentPositionY);

        computacion.SetBuffer(_kernelforPrintTextureTransparent,"bufferImageColor", bufferImageColor);
        computacion.SetBuffer(_kernelforPrintTextureTransparent,"bufferTextureTransparentPositionX", bufferTextureTransparentPositionX);
        computacion.SetBuffer(_kernelforPrintTextureTransparent,"bufferTextureTransparentPositionY", bufferTextureTransparentPositionY);



        computacion.SetBuffer(_kernelforLoadTextureCharacterPosition, "bufferTextureCharacterPositionX", bufferTextureCharacterPositionX);
        computacion.SetBuffer(_kernelforLoadTextureCharacterPosition, "bufferTextureCharacterPositionY", bufferTextureCharacterPositionY);


        computacion.SetBuffer(_kernelforPrintTilesAlphabetWriteTextField, "bufferImageColorWriteTextField", bufferImageColorWriteTextField);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetWriteTextField, "bufferTextureCharacterPositionX", bufferTextureCharacterPositionX);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetWriteTextField, "bufferTextureCharacterPositionY", bufferTextureCharacterPositionY);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetWriteTextField, "bufferTextureAlphabetColor", bufferTextureAlphabetColor);
 
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividualWriteTextField, "bufferImageColorWriteTextField", bufferImageColorWriteTextField);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividualWriteTextField, "bufferTextureCharacterPositionX", bufferTextureCharacterPositionX);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividualWriteTextField, "bufferTextureCharacterPositionY", bufferTextureCharacterPositionY);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividualWriteTextField, "bufferTextureAlphabetColor", bufferTextureAlphabetColor);
         
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividualWriteTextField, "bufferImageColorWriteTextField", bufferImageColorWriteTextField);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividualWriteTextField, "bufferTextureCharacterPositionX", bufferTextureCharacterPositionX);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividualWriteTextField, "bufferTextureCharacterPositionY", bufferTextureCharacterPositionY);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividualWriteTextField, "bufferTextureNumberColor", bufferTextureNumberColor);

        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividualWriteTextField, "bufferImageColorWriteTextField", bufferImageColorWriteTextField);
        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividualWriteTextField, "bufferTextureCharacterPositionX", bufferTextureCharacterPositionX);
        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividualWriteTextField, "bufferTextureCharacterPositionY", bufferTextureCharacterPositionY);
        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividualWriteTextField, "bufferTexturePortugueseColor", bufferTexturePortugueseColor);



        computacion.SetBuffer(_kernelforPrintTilesAlphabet,"bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTilesAlphabet,"bufferTextureCharacterPositionX", bufferTextureCharacterPositionX);
        computacion.SetBuffer(_kernelforPrintTilesAlphabet,"bufferTextureCharacterPositionY", bufferTextureCharacterPositionY);
        computacion.SetBuffer(_kernelforPrintTilesAlphabet,"bufferTextureAlphabetColor", bufferTextureAlphabetColor);

        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividual,"bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividual,"bufferTextureCharacterPositionX", bufferTextureCharacterPositionX);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividual,"bufferTextureCharacterPositionY", bufferTextureCharacterPositionY);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividual,"bufferTextureAlphabetColor", bufferTextureAlphabetColor);
        
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual, "bufferTextureCharacterPositionX", bufferTextureCharacterPositionX);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual, "bufferTextureCharacterPositionY", bufferTextureCharacterPositionY);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual, "bufferTextureNumberColor", bufferTextureNumberColor);

        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividual, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividual, "bufferTextureCharacterPositionX", bufferTextureCharacterPositionX);
        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividual, "bufferTextureCharacterPositionY", bufferTextureCharacterPositionY);
        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividual, "bufferTexturePortugueseColor", bufferTexturePortugueseColor);



        computacion.SetBuffer(_kernelforLoadTextureCharacterPosition_size14, "bufferTextureCharacterPositionX_size14", bufferTextureCharacterPositionX_size14);
        computacion.SetBuffer(_kernelforLoadTextureCharacterPosition_size14, "bufferTextureCharacterPositionY_size14", bufferTextureCharacterPositionY_size14);

        computacion.SetBuffer(_kernelforPrintTilesAlphabet_size14,"bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTilesAlphabet_size14,"bufferTextureCharacterPositionX_size14", bufferTextureCharacterPositionX_size14);
        computacion.SetBuffer(_kernelforPrintTilesAlphabet_size14,"bufferTextureCharacterPositionY_size14", bufferTextureCharacterPositionY_size14);
        computacion.SetBuffer(_kernelforPrintTilesAlphabet_size14,"bufferTextureAlphabetColor_size14", bufferTextureAlphabetColor_size14);

        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividual_size14,"bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividual_size14,"bufferTextureCharacterPositionX_size14", bufferTextureCharacterPositionX_size14);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividual_size14,"bufferTextureCharacterPositionY_size14", bufferTextureCharacterPositionY_size14);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividual_size14,"bufferTextureAlphabetColor_size14", bufferTextureAlphabetColor_size14);

        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual_size14, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual_size14, "bufferTextureCharacterPositionX_size14", bufferTextureCharacterPositionX_size14);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual_size14, "bufferTextureCharacterPositionY_size14", bufferTextureCharacterPositionY_size14);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual_size14, "bufferTextureNumberColor_size14", bufferTextureNumberColor_size14);

        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividual_size14, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividual_size14, "bufferTextureCharacterPositionX_size14", bufferTextureCharacterPositionX_size14);
        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividual_size14, "bufferTextureCharacterPositionY_size14", bufferTextureCharacterPositionY_size14);
        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividual_size14, "bufferTexturePortugueseColor_size14", bufferTexturePortugueseColor_size14);



        computacion.SetBuffer(_kernelforLoadTextureCharacterPosition_size22, "bufferTextureCharacterPositionX_size22", bufferTextureCharacterPositionX_size22);
        computacion.SetBuffer(_kernelforLoadTextureCharacterPosition_size22, "bufferTextureCharacterPositionY_size22", bufferTextureCharacterPositionY_size22);

        computacion.SetBuffer(_kernelforPrintTilesAlphabet_size22, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTilesAlphabet_size22, "bufferTextureCharacterPositionX_size22", bufferTextureCharacterPositionX_size22);
        computacion.SetBuffer(_kernelforPrintTilesAlphabet_size22, "bufferTextureCharacterPositionY_size22", bufferTextureCharacterPositionY_size22);
        computacion.SetBuffer(_kernelforPrintTilesAlphabet_size22, "bufferTextureAlphabetColor_size22", bufferTextureAlphabetColor_size22);

        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividual_size22, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividual_size22, "bufferTextureCharacterPositionX_size22", bufferTextureCharacterPositionX_size22);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividual_size22, "bufferTextureCharacterPositionY_size22", bufferTextureCharacterPositionY_size22);
        computacion.SetBuffer(_kernelforPrintTilesAlphabetIndividual_size22, "bufferTextureAlphabetColor_size22", bufferTextureAlphabetColor_size22);

        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual_size22, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual_size22, "bufferTextureCharacterPositionX_size22", bufferTextureCharacterPositionX_size22);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual_size22, "bufferTextureCharacterPositionY_size22", bufferTextureCharacterPositionY_size22);
        computacion.SetBuffer(_kernelforPrintTilesNumberIndividual_size22, "bufferTextureNumberColor_size22", bufferTextureNumberColor_size22);

        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividual_size22, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividual_size22, "bufferTextureCharacterPositionX_size22", bufferTextureCharacterPositionX_size22);
        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividual_size22, "bufferTextureCharacterPositionY_size22", bufferTextureCharacterPositionY_size22);
        computacion.SetBuffer(_kernelforPrintTilesPortugueseIndividual_size22, "bufferTexturePortugueseColor_size22", bufferTexturePortugueseColor_size22);



        computacion.SetBuffer(_kernelforLoadPointerPosition, "bufferPointerPositionX", bufferPointerPositionX);
        computacion.SetBuffer(_kernelforLoadPointerPosition, "bufferPointerPositionY", bufferPointerPositionY);

        computacion.SetBuffer(_kernelforPrintPointer, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintPointer, "bufferPointerPositionX", bufferPointerPositionX);
        computacion.SetBuffer(_kernelforPrintPointer, "bufferPointerPositionY", bufferPointerPositionY);
        computacion.SetBuffer(_kernelforPrintPointer, "bufferColorPointer", bufferColorPointer);

        computacion.SetBuffer(_kernelforPrintPointerBlack, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintPointerBlack, "bufferPointerPositionX", bufferPointerPositionX);
        computacion.SetBuffer(_kernelforPrintPointerBlack, "bufferPointerPositionY", bufferPointerPositionY);



        computacion.SetBuffer(_kernelforLoadHorizontalPosition, "bufferHorizontalPositionX", bufferHorizontalPositionX);
        computacion.SetBuffer(_kernelforLoadHorizontalPosition, "bufferHorizontalPositionY", bufferHorizontalPositionY);

        computacion.SetBuffer(_kernelforPrintHorizontal, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintHorizontal, "bufferHorizontalPositionX", bufferHorizontalPositionX);
        computacion.SetBuffer(_kernelforPrintHorizontal, "bufferHorizontalPositionY", bufferHorizontalPositionY);

        computacion.SetBuffer(_kernelforPrintHorizontalBlack, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintHorizontalBlack, "bufferHorizontalPositionX", bufferHorizontalPositionX);
        computacion.SetBuffer(_kernelforPrintHorizontalBlack, "bufferHorizontalPositionY", bufferHorizontalPositionY);


        computacion.SetBuffer(_kernelforLoadVerticalPosition, "bufferVerticalPositionX", bufferVerticalPositionX);
        computacion.SetBuffer(_kernelforLoadVerticalPosition, "bufferVerticalPositionY", bufferVerticalPositionY);

        computacion.SetBuffer(_kernelforPrintVertical, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintVertical, "bufferVerticalPositionX", bufferVerticalPositionX);
        computacion.SetBuffer(_kernelforPrintVertical, "bufferVerticalPositionY", bufferVerticalPositionY);


        computacion.SetBuffer(_kernelforPrintVerticalBlack, "bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintVerticalBlack, "bufferVerticalPositionX", bufferVerticalPositionX);
        computacion.SetBuffer(_kernelforPrintVerticalBlack, "bufferVerticalPositionY", bufferVerticalPositionY);



    }



////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// start of minigame
////////////////////////////////////////////////////////////////////////////////////////////////////////////////





////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// start of minigame
////////////////////////////////////////////////////////////////////////////////////////////////////////////////


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//  UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE  //
/////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Update is called once per frame
    void Update()
    {   

        ++frames;

        timeNow = Time.realtimeSinceStartup;

        // Debug.Log(timeNow);

        if (timeNow > lastInterval + updateInterval)
        {

            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;

        }  



        if(MethodExtend.readFromButton == 1 && activo2 == false)
        {



            loadBufferTexturePosition();

            loadBufferTextureAlphabet();

            loadBufferTextureNumber();

            loadBufferTexturePortuguese();



            loadBufferTexturePosition_size14();

            loadBufferTextureAlphabet_size14();

            loadBufferTextureNumber_size14();

            loadBufferTexturePortuguese_size14();



            loadBufferTexturePosition_size22();

            loadBufferTextureAlphabet_size22();
            
            loadBufferTextureNumber_size22();
            
            loadBufferTexturePortuguese_size22();



            loadBufferPointerPosition();
    
            paintPointerPosition(PositionPointerX, PositionPointerY, (int) timeNow);    



            loadBufferHorizontalPosition();

            loadBufferVerticalPosition();

            //////////////////////////////////////////
            // Return
            /////////////////////////////////////////

            activo = !activo;        


            string fileName_words0 = "";

            string fileName_words1 = "";

            if(MethodExtend.fileName_words0 == "")
            {

                fileName_words0 = "ITALIAN/ITALIAN1/1000EnglishMOST_1";
                fileName_words1 = "ITALIAN/ITALIAN1/1000ItalianMOST_1";

            }

            else
            {

                fileName_words0 = MethodExtend.fileName_words0;

                fileName_words1 = MethodExtend.fileName_words1;

            }

            // loadWordList(fileName0_sizeVersicle0, fileName1);

         
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            /// READ DATA WORDS
            /////////////////////////////////////////////////////////////////////////////////////////////////////


           //////////////////////////////////////////////////////////////////////////////////////////////////////
            /// READ DATA WORDS 
            //////////////////////////////////////////////////////////////////////////////////////////////////////


            activo2 = !activo2;

            // listGameObject[0].SetActive(true);
            // listGameObject2[0].SetActive(true);

            
            // paintAllWords();

            paintWordPosition_size22(700, 920, "PRESS CONTROL KEY TO MENU");
            paintWordPosition_size22(700, 970, "PRESS CONTROL KEY TO MENU");


            paintAlphabetFromTextureIndividualWriteTextField(1000, 500, 1, 1);
        }






        if(Input.GetKeyUp(KeyCode.Return))
        {
    
            if(wordCheck0 == false)                        
            {

                firstCheck();

                if(wordCheck0 == true)
                {   

                    
                    CheckBool = true;
                    wordCheck0 = false;
                                     
                }
                
                if(wordCheck0 == false && CheckBool == false)
                {

                    ScriptCommunication.activatePrefabRedStar = true;
                    CheckBool = true;

                }

            }


            paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
            
            paintPointerPosition(constPositionPointerX, constPositionPointerY,(int) timeNow); 

            PositionPointerX = constPositionPointerX;
            PositionPointerY = constPositionPointerY;

        }




        if(Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {

            ///////////////////////////////////////
            /// DATA FROM CANVAS
            ///////////////////////////////////////

            activo = false;
            activo2 = false;

            MethodExtend.readFromButton = 0;

            StringTextField = "";

            ///////////////////////////////////////
            /// DATA FROM CANVAS
            ///////////////////////////////////////



            LoadScene("MainScreen");

        }



        if(Input.GetKeyUp(KeyCode.F4))
        {

            paintNumberFromTextureIndividual(_XaxisWord22, _YaxisWord22 + 200, 1, 3);

            paintWordPosition_size22(_XaxisWord22, _YaxisWord22+100, "HOLÌ");

        }   



        if(activo == true && StringTextField.Length <= 30 && ScriptCommunication.enableKeyboard == true)
        {

            getKeyBoardInput();
            
            ScriptCommunication.sizeTextField = StringTextField.Length;

        }

        

        if(activo == true && Input.GetKeyUp(KeyCode.Backspace) && StringTextField.Length > 0)
        {

            string empty = "";
            
            for(int i = 0; i < StringTextField.Length - 1 ; ++i)
            {

                empty += StringTextField[i];

            }

            StringTextField = empty;


            _XaxisTextField += CHARACTERWIDTH; 


            int _PositionX = _XaxisTextField;
            int _PositionY = _YaxisTextField;


            paintAlphabetFromTextureIndividual(_PositionX, _PositionY,2,6);
            
            paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
            PositionPointerX += 34;
            paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 


        }



        loadImageColor();

    }


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//  UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE UPDATE  //
/////////////////////////////////////////////////////////////////////////////////////////////////////////


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//   DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY   //
/////////////////////////////////////////////////////////////////////////////////////////////////////////


    void OnDestroy()
    {   


        bufferImageColor.Release();
        bufferImageColor.Dispose();
        bufferImageColor = null;

        bufferLargeMemory.Release();
        bufferLargeMemory.Dispose();
        bufferLargeMemory = null;

        bufferImageColorWriteTextField.Release();
        bufferImageColorWriteTextField.Dispose();
        bufferImageColorWriteTextField = null;


        bufferTextureTransparentPositionX.Dispose();
        bufferTextureTransparentPositionX.Release();
        bufferTextureTransparentPositionX = null;

        bufferTextureTransparentPositionY.Dispose();
        bufferTextureTransparentPositionY.Release();
        bufferTextureTransparentPositionY = null;

        bufferTextureBigPositionX.Dispose();
        bufferTextureBigPositionX.Release();
        bufferTextureBigPositionX = null;

        bufferTextureBigPositionY.Dispose();
        bufferTextureBigPositionY.Release();
        bufferTextureBigPositionY = null;



        bufferTextureCharacterPositionX.Dispose();
        bufferTextureCharacterPositionX.Release();
        bufferTextureCharacterPositionX = null;

        bufferTextureCharacterPositionY.Dispose();
        bufferTextureCharacterPositionY.Release();
        bufferTextureCharacterPositionY = null;

        bufferTextureAlphabetColor.Dispose();
        bufferTextureAlphabetColor.Release();
        bufferTextureAlphabetColor = null;

        bufferTextureNumberColor.Dispose();
        bufferTextureNumberColor.Release();
        bufferTextureNumberColor = null;

        bufferTexturePortugueseColor.Dispose();
        bufferTexturePortugueseColor.Release();
        bufferTexturePortugueseColor = null;

        bufferTextureCharacterPositionX_size14.Dispose();
        bufferTextureCharacterPositionX_size14.Release();
        bufferTextureCharacterPositionX_size14 = null;

        bufferTextureCharacterPositionY_size14.Dispose();
        bufferTextureCharacterPositionY_size14.Release();
        bufferTextureCharacterPositionY_size14 = null;

        bufferTextureAlphabetColor_size14.Dispose();
        bufferTextureAlphabetColor_size14.Release();
        bufferTextureAlphabetColor_size14 = null;

        bufferTextureNumberColor_size14.Dispose();
        bufferTextureNumberColor_size14.Release();
        bufferTextureNumberColor_size14 = null;

        bufferTexturePortugueseColor_size14.Dispose();
        bufferTexturePortugueseColor_size14.Release();
        bufferTexturePortugueseColor_size14 = null;


        
        bufferTextureCharacterPositionX_size22.Dispose();
        bufferTextureCharacterPositionX_size22.Release();
        bufferTextureCharacterPositionX_size22 = null;

        bufferTextureCharacterPositionY_size22.Dispose();
        bufferTextureCharacterPositionY_size22.Release();
        bufferTextureCharacterPositionY_size22 = null;

        bufferTextureAlphabetColor_size22.Dispose();
        bufferTextureAlphabetColor_size22.Release();
        bufferTextureAlphabetColor_size22 = null;

        bufferTextureNumberColor_size22.Dispose();
        bufferTextureNumberColor_size22.Release();        
        bufferTextureNumberColor_size22 = null;

        bufferTexturePortugueseColor_size22.Dispose();
        bufferTexturePortugueseColor_size22.Release();
        bufferTexturePortugueseColor_size22 = null;



        bufferPointerPositionX.Dispose();
        bufferPointerPositionX.Release();
        bufferPointerPositionX = null;

        bufferPointerPositionY.Dispose();
        bufferPointerPositionY.Release();
        bufferPointerPositionY = null;

        bufferColorPointer.Dispose();
        bufferColorPointer.Release();
        bufferColorPointer = null;


        
        bufferHorizontalPositionX.Dispose();
        bufferHorizontalPositionX.Release();
        bufferHorizontalPositionX = null;
    
        bufferHorizontalPositionY.Dispose();
        bufferHorizontalPositionY.Release();
        bufferHorizontalPositionY = null;

        bufferVerticalPositionX.Dispose();
        bufferVerticalPositionX.Release();
        bufferVerticalPositionX = null;
    
        bufferVerticalPositionY.Dispose();
        bufferVerticalPositionY.Release();
        bufferVerticalPositionY = null;
    
    }




/////////////////////////////////////////////////////////////////////////////////////////////////////////
//   DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY   //
/////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    

    public void loadImageColor()
    {
     
        computacion.Dispatch(_kernelforLoadImageColor ,2000, 1000, 1);
    
    }    


    public void loadTextureBig()
    {

        computacion.Dispatch(_kernelforLoadTextureBig, 2000, 1000, 1);

    }


    public void paintTextureBig()
    {

        computacion.SetInt("_PositionBigX", 2000);
        computacion.SetInt("_PositionBigY", 1000);

        computacion.Dispatch(_kernelforPrintTextureBig , 2000 , 1000, 1);

    }


    public void loadTransparent()
    {
        computacion.Dispatch(_kernelforLoadTextureTransparent, 1000, 1, 1);
    }


    public void paintTransparent()
    {

        int _PositionTransparentX = 0;
        int _PositionTransparentY = 0;
        
        for(int i = 0; i < 1; ++i)
        {
            
            computacion.SetInt("_PositionTransparentX", _PositionTransparentX);
            computacion.SetInt("_PositionTransparentY", _PositionTransparentY);

            computacion.Dispatch(_kernelforPrintTextureTransparent,1000, 1, 1);
            _PositionTransparentX += 1000;                
        }

    }
        

////////////////////////////////////////////////////////////////////////////////////////////////////////////
//// PILOTING TEXFIELD
////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public void firstCheck()
    {


        string operation_StringTextField = StringTextField;

        bool firstCheckBool = false;
    

        if(firstCheckBool == true)
        {

            wordCheck0 = true;
            CheckBool = true;


            StringTextField = "";
            
            _XaxisTextField = _XaxisTextFieldBegin;

            paintWordPosition(_XaxisTextFieldBegin, _YaxisTextFieldBegin, "                               ");
                
        }
    
        
        if(firstCheckBool == false)
        {
            CheckBool = false;

            StringTextField = "";
            
            _XaxisTextField = _XaxisTextFieldBegin;

            paintWordPosition(_XaxisTextFieldBegin, _YaxisTextFieldBegin, "                               ");

        }
        

    }




////////////////////////////////////////////////////////////////////////////////////////////////////////////
//// PILOTING TEXFIELD
////////////////////////////////////////////////////////////////////////////////////////////////////////////




///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////





///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////





    public void readDataColorTextureCharacters(string fileNameColor, ref float[] loadTextureCharacter, uint numOfImagePixel)
    {

        //IMAGEDATA
        //letterQPosition

        string fileName = fileNameColor;

        string path = fileName;

        //dataToLoadColor;

        //Read the text from directly from the test file
        TextAsset reader = Resources.Load<TextAsset>(path);

        string lecturaDatos = reader.text;

        //float[] dataToSet = new float[5456];
        // reader.Close();
 
        string lecturaString = "";

        int countDataToSet = 0;


        //38192
        for(int i = 0 ; i < numOfImagePixel; ++i)
        {

            for(int j = 0 ; j < 53; ++j )
            {

                if(j == 0) 
                {
                }
                else if(j % 13 == 0)
                {

                    loadTextureCharacter[countDataToSet] = float.Parse(lecturaString);

                    lecturaString = "";
                    countDataToSet ++;

                }
                else
                {
                    lecturaString += lecturaDatos[i * 54 + j];

                }

            }

        }

    }


////////////////////////////////////////////////////////////////////////////////////////////////
// MEMORY OF characters TEXTURE 36 SIZE
////////////////////////////////////////////////////////////////////////////////////////////////



    public void loadBufferTexturePosition()
    {
        
        computacion.Dispatch(_kernelforLoadTextureCharacterPosition, 392, 1, 1);

    }
    



    public void loadBufferTextureAlphabet()
    {

        string fileTextureAlphabet = "TextureOfCharactersColor";
        
        float[] arrayTextureAlphabetColor = new float[175616];
        //positions[1692800];
        uint numOfImagePixel = 43904;

        readDataColorTextureCharacters(fileTextureAlphabet, ref arrayTextureAlphabetColor, numOfImagePixel);

        bufferTextureAlphabetColor.SetData(arrayTextureAlphabetColor);

    }




    public void paintAlphabetFromTexture()
    {

        computacion.Dispatch(_kernelforPrintTilesAlphabet, 392, 1,1);

    }


    public void paintAlphabetFromTextureWriteTextField()
    {

        computacion.Dispatch(_kernelforPrintTilesAlphabetWriteTextField, 392, 1,1);

    }




    public void paintAlphabetFromTextureIndividualWriteTextField(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenXWriteTextField = Xaxis;        
        int _PositionScreenYWriteTextField = Yaxis;
        
        int _PositionTileXWriteTextField = Xtile;        
        int _PositionTileYWriteTextField = Ytile;                
        

        computacion.SetInt("_PositionScreenXWriteTextField", _PositionScreenXWriteTextField);
        computacion.SetInt("_PositionScreenYWriteTextField", _PositionScreenYWriteTextField);

        computacion.SetInt("_PositionTileXWriteTextField", _PositionTileXWriteTextField);
        computacion.SetInt("_PositionTileYWriteTextField", _PositionTileYWriteTextField);

        computacion.Dispatch(_kernelforPrintTilesAlphabetIndividualWriteTextField, 56, 1,1);

    }



    public void paintAlphabetFromTextureIndividual(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenX = Xaxis;        
        int _PositionScreenY = Yaxis;
        
        int _PositionTileX = Xtile;        
        int _PositionTileY = Ytile;                
        

        computacion.SetInt("_PositionScreenX", _PositionScreenX);
        computacion.SetInt("_PositionScreenY", _PositionScreenY);

        computacion.SetInt("_PositionTileX", _PositionTileX);
        computacion.SetInt("_PositionTileY", _PositionTileY);

        computacion.Dispatch(_kernelforPrintTilesAlphabetIndividual, 56, 1,1);

    }



    //forLoadTextureNumberPosition
    public void loadBufferTextureNumber()
    {

        string fileTextureNumber = "TextureOfNumbersColor";
        
        float[] arrayTextureNumberColor = new float[175616];
        //positions[1692800];

        uint numOfImagePixel = 43904;
        
        readDataColorTextureCharacters(fileTextureNumber, ref arrayTextureNumberColor, numOfImagePixel);

        bufferTextureNumberColor.SetData(arrayTextureNumberColor);

    }




    public void paintNumberFromTextureIndividualWriteTextField(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenXWriteTextField = Xaxis;
        int _PositionScreenYWriteTextField = Yaxis;
        
        int _PositionTileXWriteTextField = Xtile;        
        int _PositionTileYWriteTextField = Ytile;                
        

        computacion.SetInt("_PositionScreenNumberXWriteTextField", _PositionScreenXWriteTextField);
        computacion.SetInt("_PositionScreenNumberYWriteTextField", _PositionScreenYWriteTextField);

        computacion.SetInt("_PositionTileNumberXWriteTextField", _PositionTileXWriteTextField);
        computacion.SetInt("_PositionTileNumberYWriteTextField", _PositionTileYWriteTextField);

        computacion.Dispatch(_kernelforPrintTilesNumberIndividualWriteTextField, 56, 1,1);

    }





    public void paintNumberFromTextureIndividual(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenX = Xaxis;
        int _PositionScreenY = Yaxis;
        
        int _PositionTileX = Xtile;        
        int _PositionTileY = Ytile;                
        

        computacion.SetInt("_PositionScreenNumberX", _PositionScreenX);
        computacion.SetInt("_PositionScreenNumberY", _PositionScreenY);

        computacion.SetInt("_PositionTileNumberX", _PositionTileX);
        computacion.SetInt("_PositionTileNumberY", _PositionTileY);

        computacion.Dispatch(_kernelforPrintTilesNumberIndividual, 56, 1,1);

    }





    public void loadBufferTexturePortuguese()
    {

        string fileTexturePortuguese = "TextureOfPortugueseColor";
        
        float[] arrayTexturePortugueseColor = new float[152768];
        //positions[1692800];

        uint numOfImagePixel = 38192;
        
        readDataColorTextureCharacters(fileTexturePortuguese, ref arrayTexturePortugueseColor, numOfImagePixel);

        bufferTexturePortugueseColor.SetData(arrayTexturePortugueseColor);

    }





    public void paintPortugueseFromTextureIndividualWriteTextField(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenXWriteTextField = Xaxis;        
        int _PositionScreenYWriteTextField = Yaxis;
        
        int _PositionTileXWriteTextField = Xtile;        
        int _PositionTileYWriteTextField = Ytile;                
        

        computacion.SetInt("_PositionScreenPortugueseXWriteTextField", _PositionScreenXWriteTextField);
        computacion.SetInt("_PositionScreenPortugueseYWriteTextField", _PositionScreenYWriteTextField);

        computacion.SetInt("_PositionTilePortugueseXWriteTextField", _PositionTileXWriteTextField);
        computacion.SetInt("_PositionTilePortugueseYWriteTextField", _PositionTileYWriteTextField);

        computacion.Dispatch(_kernelforPrintTilesPortugueseIndividualWriteTextField, 44, 1,1);

    }





    public void paintPortugueseFromTextureIndividual(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenX = Xaxis;        
        int _PositionScreenY = Yaxis;
        
        int _PositionTileX = Xtile;        
        int _PositionTileY = Ytile;                
        

        computacion.SetInt("_PositionScreenPortugueseX", _PositionScreenX);
        computacion.SetInt("_PositionScreenPortugueseY", _PositionScreenY);

        computacion.SetInt("_PositionTilePortugueseX", _PositionTileX);
        computacion.SetInt("_PositionTilePortugueseY", _PositionTileY);

        computacion.Dispatch(_kernelforPrintTilesPortugueseIndividual, 44, 1,1);

    }



////////////////////////////////////////////////////////////////////////////////////////////////
// MEMORY OF characters TEXTURE 36 SIZE
////////////////////////////////////////////////////////////////////////////////////////////////


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// MEMORY character TEXTURE 14 SIZE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




    public void loadBufferTexturePosition_size14()
    {
        
        computacion.Dispatch(_kernelforLoadTextureCharacterPosition_size14, 147, 1, 1);

    }
    



    public void loadBufferTextureAlphabet_size14()
    {

        string fileTextureAlphabet = "TransparentTextureOfCharacters-size14Color";
        
        float[] arrayTextureAlphabetColor = new float[25872];
        //positions[1692800];
        uint numOfImagePixel = 6468;

        readDataColorTextureCharacters(fileTextureAlphabet, ref arrayTextureAlphabetColor, numOfImagePixel);

        bufferTextureAlphabetColor_size14.SetData(arrayTextureAlphabetColor);

    }





    public void paintAlphabetFromTexture_size14()
    {

        computacion.Dispatch(_kernelforPrintTilesAlphabet_size14, 147, 1,1);

    }





    public void paintAlphabetFromTextureIndividual_size14(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenX = Xaxis;        
        int _PositionScreenY = Yaxis;
        
        int _PositionTileX = Xtile;        
        int _PositionTileY = Ytile;                
        

        computacion.SetInt("_PositionScreenX_size14", _PositionScreenX);
        computacion.SetInt("_PositionScreenY_size14", _PositionScreenY);

        computacion.SetInt("_PositionTileX_size14", _PositionTileX);
        computacion.SetInt("_PositionTileY_size14", _PositionTileY);

        computacion.Dispatch(_kernelforPrintTilesAlphabetIndividual_size14, 21, 1,1);

    }




    //forLoadTextureNumberPosition
    public void loadBufferTextureNumber_size14()
    {

        string fileTextureNumber = "TransparentTextureOfNumbers-size14Color";
        
        float[] arrayTextureNumberColor = new float[25872];
        //positions[1692800];

        uint numOfImagePixel = 6468;
        
        readDataColorTextureCharacters(fileTextureNumber, ref arrayTextureNumberColor, numOfImagePixel);

        bufferTextureNumberColor_size14.SetData(arrayTextureNumberColor);

    }





    public void paintNumberFromTextureIndividual_size14(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenX = Xaxis;
        int _PositionScreenY = Yaxis;
        
        int _PositionTileX = Xtile;        
        int _PositionTileY = Ytile;                
        

        computacion.SetInt("_PositionScreenNumberX_size14", _PositionScreenX);
        computacion.SetInt("_PositionScreenNumberY_size14", _PositionScreenY);

        computacion.SetInt("_PositionTileNumberX_size14", _PositionTileX);
        computacion.SetInt("_PositionTileNumberY_size14", _PositionTileY);

        computacion.Dispatch(_kernelforPrintTilesNumberIndividual_size14, 21, 1,1);

    }




    //forLoadTextureNumberPosition
    public void loadBufferTexturePortuguese_size14()
    {

        string fileTexturePortuguese = "TransparentTextureOfPortuguese-size14Color";
        
        float[] arrayTexturePortugueseColor = new float[25872];
        //positions[1692800];

        uint numOfImagePixel = 6468;
        
        readDataColorTextureCharacters(fileTexturePortuguese, ref arrayTexturePortugueseColor, numOfImagePixel);

        bufferTexturePortugueseColor_size14.SetData(arrayTexturePortugueseColor);

    }





    public void paintPortugueseFromTextureIndividual_size14(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenX = Xaxis;        
        int _PositionScreenY = Yaxis;
        
        int _PositionTileX = Xtile;        
        int _PositionTileY = Ytile;                
        

        computacion.SetInt("_PositionScreenPortugueseX_size14", _PositionScreenX);
        computacion.SetInt("_PositionScreenPortugueseY_size14", _PositionScreenY);

        computacion.SetInt("_PositionTilePortugueseX_size14", _PositionTileX);
        computacion.SetInt("_PositionTilePortugueseY_size14", _PositionTileY);

        computacion.Dispatch(_kernelforPrintTilesPortugueseIndividual_size14, 21, 1,1);

    }



//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// MEMORY character TEXTURE 14 SIZE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// MEMORY character TEXTURE 22 SIZE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




    public void loadBufferTexturePosition_size22()
    {
        
        computacion.Dispatch(_kernelforLoadTextureCharacterPosition_size22, 238, 1, 1);

    }
    



    public void loadBufferTextureAlphabet_size22()
    {

        string fileTextureAlphabet = "TransparentTextureOfCharacters-size22Color";
        
        float[] arrayTextureAlphabetColor = new float[64736];
        //positions[1692800];
        uint numOfImagePixel = 16184;

        readDataColorTextureCharacters(fileTextureAlphabet, ref arrayTextureAlphabetColor, numOfImagePixel);

        bufferTextureAlphabetColor_size22.SetData(arrayTextureAlphabetColor);

    }





    public void paintAlphabetFromTexture_size22()
    {

        computacion.Dispatch(_kernelforPrintTilesAlphabet_size22, 238, 1,1);

    }





    public void paintAlphabetFromTextureIndividual_size22(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenX = Xaxis;        
        int _PositionScreenY = Yaxis;
        
        int _PositionTileX = Xtile;        
        int _PositionTileY = Ytile;                
        

        computacion.SetInt("_PositionScreenX_size22", _PositionScreenX);
        computacion.SetInt("_PositionScreenY_size22", _PositionScreenY);

        computacion.SetInt("_PositionTileX_size22", _PositionTileX);
        computacion.SetInt("_PositionTileY_size22", _PositionTileY);

        computacion.Dispatch(_kernelforPrintTilesAlphabetIndividual_size22, 34, 1,1);

    }




    //forLoadTextureNumberPosition
    public void loadBufferTextureNumber_size22()
    {

        string fileTextureNumber = "TransparentTextureOfNumbers-size22Color";
        
        float[] arrayTextureNumberColor = new float[64736];
        //positions[1692800];

        uint numOfImagePixel = 16184;
        
        readDataColorTextureCharacters(fileTextureNumber, ref arrayTextureNumberColor, numOfImagePixel);

        bufferTextureNumberColor_size22.SetData(arrayTextureNumberColor);

    }





    public void paintNumberFromTextureIndividual_size22(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenX = Xaxis;
        int _PositionScreenY = Yaxis;
        
        int _PositionTileX = Xtile;        
        int _PositionTileY = Ytile;                
        

        computacion.SetInt("_PositionScreenNumberX_size22", _PositionScreenX);
        computacion.SetInt("_PositionScreenNumberY_size22", _PositionScreenY);

        computacion.SetInt("_PositionTileNumberX_size22", _PositionTileX);
        computacion.SetInt("_PositionTileNumberY_size22", _PositionTileY);

        computacion.Dispatch(_kernelforPrintTilesNumberIndividual_size22, 34, 1,1);

    }




    //forLoadTextureNumberPosition
    public void loadBufferTexturePortuguese_size22()
    {

        string fileTexturePortuguese = "TransparentTextureOfPortuguese-size22Color";
        
        float[] arrayTexturePortugueseColor = new float[64736];
        //positions[1692800];

        uint numOfImagePixel = 16184;
        
        readDataColorTextureCharacters(fileTexturePortuguese, ref arrayTexturePortugueseColor, numOfImagePixel);

        bufferTexturePortugueseColor_size22.SetData(arrayTexturePortugueseColor);

    }





    public void paintPortugueseFromTextureIndividual_size22(int Xaxis, int Yaxis, int Xtile, int Ytile)
    {

        int _PositionScreenX = Xaxis;        
        int _PositionScreenY = Yaxis;
        
        int _PositionTileX = Xtile;        
        int _PositionTileY = Ytile;                
        

        computacion.SetInt("_PositionScreenPortugueseX_size22", _PositionScreenX);
        computacion.SetInt("_PositionScreenPortugueseY_size22", _PositionScreenY);

        computacion.SetInt("_PositionTilePortugueseX_size22", _PositionTileX);
        computacion.SetInt("_PositionTilePortugueseY_size22", _PositionTileY);

        computacion.Dispatch(_kernelforPrintTilesPortugueseIndividual_size22, 34, 1,1);

    }


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// MEMORY character TEXTURE 22 SIZE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// MEMORY POINTER
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public void loadBufferPointerPosition()
    {

        computacion.Dispatch(_kernelforLoadPointerPosition, 44, 1, 1);        

    }



    public void paintPointerPosition(int Xaxis, int Yaxis, int seed)
    {


        int _PositionScreenX_Pointer = Xaxis;
        int _PositionScreenY_Pointer = Yaxis;


        computacion.SetInt("_PositionScreenX_Pointer", _PositionScreenX_Pointer);
        computacion.SetInt("_PositionScreenY_Pointer", _PositionScreenY_Pointer);


        System.Random aleatorioNumber = new System.Random(seed);


        int _arrayColor0 = aleatorioNumber.Next(0,255);

        
        int _arrayColor1 = aleatorioNumber.Next(0,255);

        
        int _arrayColor2 = aleatorioNumber.Next(0,255);
        
        float[] colorData = {_arrayColor0, _arrayColor1, _arrayColor2};

        bufferColorPointer.SetData(colorData, 0, 0, 3 );


        computacion.Dispatch(_kernelforPrintPointer, 44, 1, 1);        


    }



    public void paintPointerPositionBlack(int Xaxis, int Yaxis)
    {


        int _PositionScreenX_PointerBlack = Xaxis;
        int _PositionScreenY_PointerBlack = Yaxis;

        computacion.SetInt("_PositionScreenX_PointerBlack", _PositionScreenX_PointerBlack);
        computacion.SetInt("_PositionScreenY_PointerBlack", _PositionScreenY_PointerBlack);

        computacion.Dispatch(_kernelforPrintPointerBlack, 44, 1, 1);        


    }


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// MEMORY POINTER
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// MEMORY BOUNDS
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public void loadBufferHorizontalPosition()
    {

        computacion.Dispatch(_kernelforLoadHorizontalPosition, 7, 1, 1);        

    }

    
    public void loadBufferVerticalPosition()
    {

        computacion.Dispatch(_kernelforLoadVerticalPosition, 40, 1, 1);        

    }


    public void paintHorizontalPosition(int Xaxis, int Yaxis)
    {

        int _PositionScreenX_Horizontal = Xaxis;
        int _PositionScreenY_Horizontal = Yaxis;

        computacion.SetInt("_PositionScreenX_Horizontal", _PositionScreenX_Horizontal);
        computacion.SetInt("_PositionScreenY_Horizontal", _PositionScreenY_Horizontal);

        computacion.Dispatch(_kernelforPrintHorizontal, 7, 1, 1);        

    }


    public void paintVerticalPosition(int Xaxis, int Yaxis)
    {

        int _PositionScreenX_Vertical = Xaxis;
        int _PositionScreenY_Vertical = Yaxis;

        computacion.SetInt("_PositionScreenX_Vertical", _PositionScreenX_Vertical);
        computacion.SetInt("_PositionScreenY_Vertical", _PositionScreenY_Vertical);

        computacion.Dispatch(_kernelforPrintVertical, 40, 1, 1);        

    }


    public void paintHorizontalPositionBlack(int Xaxis, int Yaxis)
    {

        int _PositionScreenX_HorizontalBlack = Xaxis;
        int _PositionScreenY_HorizontalBlack = Yaxis;

        computacion.SetInt("_PositionScreenX_HorizontalBlack", _PositionScreenX_HorizontalBlack);
        computacion.SetInt("_PositionScreenY_HorizontalBlack", _PositionScreenY_HorizontalBlack);

        computacion.Dispatch(_kernelforPrintHorizontalBlack, 7, 1, 1);        

    }


    public void paintVerticalPositionBlack(int Xaxis, int Yaxis)
    {

        int _PositionScreenX_VerticalBlack = Xaxis;
        int _PositionScreenY_VerticalBlack = Yaxis;

        computacion.SetInt("_PositionScreenX_VerticalBlack", _PositionScreenX_VerticalBlack);
        computacion.SetInt("_PositionScreenY_VerticalBlack", _PositionScreenY_VerticalBlack);

        computacion.Dispatch(_kernelforPrintVerticalBlack, 40, 1, 1);        

    }



//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// MEMORY BOUNDS
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public void paintWordPosition(int Xaxis, int Yaxis, string manageString)
    {

        // System.Random random = new System.Random();
        // int _PositionTranslateX = 965; 
        // int _PositionTranslateY = 20;



        int _PositionTranslateX = Xaxis; 
        int _PositionTranslateY = Yaxis;


        string inputString = manageString;


        for(int i = 0; i < inputString.Length; ++i) 
        {

            string characterToPaint = inputString[i].ToString();

            if(characterToPaint == "A")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 0);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "B")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 0);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "C")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 0);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "D")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 0);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "E")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 1);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "F")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 1);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "G")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 1);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "H")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 1);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "I")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 2);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "J")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 2);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "K")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 2);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "L")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 2);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "M")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 3);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "N")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 3);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "O")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 3);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "P")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 3);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "Q")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 4);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "R")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 4);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "S")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 4);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "T")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 4);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "U")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 5);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "V")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 5);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "W")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 5);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "X")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 5);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "Y")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 6);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == "Z")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 6);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            if(characterToPaint == " ")
            {
                paintAlphabetFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 6);
                _PositionTranslateX -= CHARACTERWIDTH;
            }

            /////////////////////////////////////////////////////////////////////////////////////////
            // Texture of numbers and italian
            /////////////////////////////////////////////////////////////////////////////////////////


            if(MethodExtend.keyboardLanguage == 0)
            {

    
                if(characterToPaint == "0")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 0);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "1")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 0);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "2")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 0);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "3")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 0);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "4")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 1);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "5")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 1);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "6")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 1);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "7")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 1);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "8")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 2);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "9")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 2);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "À")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 2);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "È")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 2);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "É")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 3);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "Ì")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 3);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "Ò")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 3);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "Ù")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 3);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "’")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 4);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
                if(characterToPaint == "‘")
                {
                    paintNumberFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 4);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }

    
            }

            if(MethodExtend.keyboardLanguage == 1)
            {

                if(characterToPaint == "Á")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 0);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
            
                if(characterToPaint == "À")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 0);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
            
                if(characterToPaint == "Â")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 0);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }

                if(characterToPaint == "Ã")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 1);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
            
                if(characterToPaint == "Ä")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 0);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
            
                if(characterToPaint == "Ç")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 1);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
    
                if(characterToPaint == "É")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 1);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
            
                if(characterToPaint == "È")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 1);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
            
                if(characterToPaint == "Ê")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 2);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }

                if(characterToPaint == "Ë")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 2);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }    

                if(characterToPaint == "Í")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 2);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
            
                if(characterToPaint == "Ì")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 2);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
            
                if(characterToPaint == "Î")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 3);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }

                if(characterToPaint == "Ï")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 3);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }    

                if(characterToPaint == "Ó")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 3);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
            
                if(characterToPaint == "Ò")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 3, 3);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
        
                if(characterToPaint == "Õ")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 4);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }

                if(characterToPaint == "Ô")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 4);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }    

                if(characterToPaint == "Ö")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 4);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }    

                if(characterToPaint == "Ú")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 4);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
            
                if(characterToPaint == "Ù")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 0, 5);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }
            
                if(characterToPaint == "Û")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 1, 5);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }

                if(characterToPaint == "Ü")
                {
                    paintPortugueseFromTextureIndividual(_PositionTranslateX, _PositionTranslateY, 2, 5);
                    _PositionTranslateX -= CHARACTERWIDTH;
                }    

            }

        }

    }    




    public void paintWordPosition_size14(int Xaxis, int Yaxis, string manageString)
    {

        // System.Random random = new System.Random();
        // int _PositionTranslateX = 965; 
        // int _PositionTranslateY = 20;



        int _PositionTranslateX = Xaxis; 
        int _PositionTranslateY = Yaxis;


        string inputString = manageString;


        for(int i = 0; i < inputString.Length; ++i) 
        {

            string characterToPaint = inputString[i].ToString();

            if(characterToPaint == "A")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 0);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "B")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 0);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "C")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 0);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "D")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 0);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "E")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 1);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "F")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 1);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "G")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 1);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "H")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 1);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "I")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 2);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "J")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 2);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "K")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 2);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "L")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 2);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "M")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 3);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "N")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 3);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "O")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 3);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "P")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 3);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "Q")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 4);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "R")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 4);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "S")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 4);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "T")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 4);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "U")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 5);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "V")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 5);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "W")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 5);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "X")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 5);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "Y")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 6);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == "Z")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 6);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            if(characterToPaint == " ")
            {
                paintAlphabetFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 6);
                _PositionTranslateX -= CHARACTERWIDTH_size14;
            }

            /////////////////////////////////////////////////////////////////////////////////////////
            // Texture of numbers and italian
            /////////////////////////////////////////////////////////////////////////////////////////


            if(MethodExtend.keyboardLanguage == 0)
            {

    
                if(characterToPaint == "0")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "1")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "2")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "3")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "4")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "5")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "6")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "7")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "8")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "9")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "À")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "È")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "É")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "Ì")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "Ò")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "Ù")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "’")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
                if(characterToPaint == "‘")
                {
                    paintNumberFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }

    
            }

            if(MethodExtend.keyboardLanguage == 1)
            {

                if(characterToPaint == "Á")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
            
                if(characterToPaint == "À")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
            
                if(characterToPaint == "Â")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }

                if(characterToPaint == "Ã")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
            
                if(characterToPaint == "Ä")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
            
                if(characterToPaint == "Ç")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
    
                if(characterToPaint == "É")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
            
                if(characterToPaint == "È")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
            
                if(characterToPaint == "Ê")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }

                if(characterToPaint == "Ë")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }    

                if(characterToPaint == "Í")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
            
                if(characterToPaint == "Ì")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
            
                if(characterToPaint == "Î")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }

                if(characterToPaint == "Ï")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }    

                if(characterToPaint == "Ó")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
            
                if(characterToPaint == "Ò")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 3, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
        
                if(characterToPaint == "Õ")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }

                if(characterToPaint == "Ô")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }    

                if(characterToPaint == "Ö")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }    

                if(characterToPaint == "Ú")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
            
                if(characterToPaint == "Ù")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 0, 5);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }
            
                if(characterToPaint == "Û")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 1, 5);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }

                if(characterToPaint == "Ü")
                {
                    paintPortugueseFromTextureIndividual_size14(_PositionTranslateX, _PositionTranslateY, 2, 5);
                    _PositionTranslateX -= CHARACTERWIDTH_size14;
                }    

            }

        }

    }    


    public void paintWordPosition_size22(int Xaxis, int Yaxis, string manageString)
    {

        // System.Random random = new System.Random();
        // int _PositionTranslateX = 965; 
        // int _PositionTranslateY = 20;



        int _PositionTranslateX = Xaxis; 
        int _PositionTranslateY = Yaxis;


        string inputString = manageString;


        for(int i = 0; i < inputString.Length; ++i) 
        {

            string characterToPaint = inputString[i].ToString();

            if(characterToPaint == "A")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 0);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "B")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 0);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "C")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 0);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "D")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 0);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "E")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 1);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "F")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 1);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "G")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 1);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "H")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 1);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "I")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 2);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "J")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 2);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "K")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 2);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "L")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 2);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "M")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 3);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "N")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 3);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "O")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 3);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "P")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 3);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "Q")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 4);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "R")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 4);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "S")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 4);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "T")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 4);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "U")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 5);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "V")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 5);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "W")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 5);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "X")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 5);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "Y")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 6);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == "Z")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 6);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            if(characterToPaint == " ")
            {
                paintAlphabetFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 6);
                _PositionTranslateX -= CHARACTERWIDTH_size22;
            }

            /////////////////////////////////////////////////////////////////////////////////////////
            // Texture of numbers and italian
            /////////////////////////////////////////////////////////////////////////////////////////


            if(MethodExtend.keyboardLanguage == 0)
            {

    
                if(characterToPaint == "0")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "1")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "2")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "3")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "4")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "5")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "6")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "7")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "8")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "9")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "À")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "È")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "É")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "Ì")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "Ò")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "Ù")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "’")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

                if(characterToPaint == "‘")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

                if(characterToPaint == "?")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

                if(characterToPaint == "!")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

                if(characterToPaint == ",")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 5);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

                if(characterToPaint == ".")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 5);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

                if(characterToPaint == ":")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 5);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

                if(characterToPaint == ";")
                {
                    paintNumberFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 5);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

            }

            if(MethodExtend.keyboardLanguage == 1)
            {

                if(characterToPaint == "Á")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
            
                if(characterToPaint == "À")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
            
                if(characterToPaint == "Â")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

                if(characterToPaint == "Ã")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
            
                if(characterToPaint == "Ä")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 0);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
            
                if(characterToPaint == "Ç")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
    
                if(characterToPaint == "É")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
            
                if(characterToPaint == "È")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 1);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
            
                if(characterToPaint == "Ê")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

                if(characterToPaint == "Ë")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }    

                if(characterToPaint == "Í")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
            
                if(characterToPaint == "Ì")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 2);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
            
                if(characterToPaint == "Î")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

                if(characterToPaint == "Ï")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }    

                if(characterToPaint == "Ó")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
            
                if(characterToPaint == "Ò")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 3, 3);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
        
                if(characterToPaint == "Õ")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

                if(characterToPaint == "Ô")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }    

                if(characterToPaint == "Ö")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }    

                if(characterToPaint == "Ú")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 4);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
            
                if(characterToPaint == "Ù")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 0, 5);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }
            
                if(characterToPaint == "Û")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 1, 5);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }

                if(characterToPaint == "Ü")
                {
                    paintPortugueseFromTextureIndividual_size22(_PositionTranslateX, _PositionTranslateY, 2, 5);
                    _PositionTranslateX -= CHARACTERWIDTH_size22;
                }    

            }

        }

    }    




    public void getKeyBoardInput()
    {

        //////////////////////////////////////////////////////////////////////////
        // PORTUGUESE CHARACTERS
        //////////////////////////////////////////////////////////////////////////
            
                                  //A Á À Ã Â Ä       
            int[] getAppostrophe = {0,1,2,3,4,5};    

            if(afterAppostrophe == true)
            {
                afterAppostrophe = false;
                appostrophe = 0;
            }        
            
            if( Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
            {
            
                shiftKey = true;
            
            }
            else
            {
            
                shiftKey = false;
            
            }


            if(MethodExtend.keyboardLanguage == 1)
            {
                
                if(Input.GetKeyUp(KeyCode.LeftBracket) && shiftKey == false)
                {

                    appostrophe = 1;

                }
                
                if(Input.GetKeyUp(KeyCode.LeftBracket) && shiftKey == true)
                {

                    appostrophe = 2;

                }                

                if(Input.GetKeyUp(KeyCode.Quote) && shiftKey == false)
                {

                    appostrophe = 3;

                }
                
                if(Input.GetKeyUp(KeyCode.Quote) && shiftKey == true)
                {

                    appostrophe = 4;

                }                

                if(Input.GetKeyUp(KeyCode.Alpha6) && shiftKey == true)
                {

                    appostrophe = 5;

                }                



                if(Input.GetKeyUp(KeyCode.A) && appostrophe == 1 )
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 0, 0);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Á";
                    afterAppostrophe = true;

    
                }
            
                if(Input.GetKeyUp(KeyCode.A) && appostrophe == 2)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 1, 0);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "À";
                    afterAppostrophe = true;

    
                }
            
                if(Input.GetKeyUp(KeyCode.A) && appostrophe == 4)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 2, 0);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Â";
                    afterAppostrophe = true;

    
                }

                if(Input.GetKeyUp(KeyCode.A) && appostrophe == 3)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 0, 1);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ã";
                    afterAppostrophe = true;

    
                }

                if(Input.GetKeyUp(KeyCode.A) && appostrophe == 5)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 3, 0);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ä";
                    afterAppostrophe = true;

    
                }
            
                if(Input.GetKeyUp(KeyCode.Semicolon))
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 1, 1);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ç";
                    afterAppostrophe = true;

    
                }
    
                if(Input.GetKeyUp(KeyCode.E) && appostrophe == 1 )
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 2, 1);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "É";
                    afterAppostrophe = true;

    
                }
            
                if(Input.GetKeyUp(KeyCode.E) && appostrophe == 2)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 3, 1);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "È";
                    afterAppostrophe = true;

    
                }
            
                if(Input.GetKeyUp(KeyCode.E) && appostrophe == 4)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 0, 2);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ê";
                    afterAppostrophe = true;

    
                }

                if(Input.GetKeyUp(KeyCode.E) && appostrophe == 3)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 0, 2);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ê";
                    afterAppostrophe = true;

    
                }    

                if(Input.GetKeyUp(KeyCode.E) && appostrophe == 5)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 1, 2);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ë";
                    afterAppostrophe = true;

    
                }    

                if(Input.GetKeyUp(KeyCode.I) && appostrophe == 1 )
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 2, 2);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Í";
                    afterAppostrophe = true;

    
                }
            
                if(Input.GetKeyUp(KeyCode.I) && appostrophe == 2)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 3, 2);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ì";
                    afterAppostrophe = true;

    
                }
            
                if(Input.GetKeyUp(KeyCode.I) && appostrophe == 4)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 0, 3);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Î";
                    afterAppostrophe = true;

    
                }

                if(Input.GetKeyUp(KeyCode.I) && appostrophe == 3)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 0, 3);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Î";
                    afterAppostrophe = true;

    
                }    

                if(Input.GetKeyUp(KeyCode.I) && appostrophe == 5)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 1, 3);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ï";
                    afterAppostrophe = true;

    
                }    

                if(Input.GetKeyUp(KeyCode.O) && appostrophe == 1 )
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 2, 3);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ó";
                    afterAppostrophe = true;

    
                }
            
                if(Input.GetKeyUp(KeyCode.O) && appostrophe == 2)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 3, 3);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ò";
                    afterAppostrophe = true;

    
                }
            
                if(Input.GetKeyUp(KeyCode.O) && appostrophe == 4)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 2, 4);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Õ";
                    afterAppostrophe = true;

    
                }

                if(Input.GetKeyUp(KeyCode.O) && appostrophe == 3)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 0, 4);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ô";
                    afterAppostrophe = true;

    
                }    

                if(Input.GetKeyUp(KeyCode.O) && appostrophe == 5)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 1, 4);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ö";
                    afterAppostrophe = true;

    
                }    

                if(Input.GetKeyUp(KeyCode.U) && appostrophe == 1 )
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 1, 4);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ú";
                    afterAppostrophe = true;

    
                }
            
                if(Input.GetKeyUp(KeyCode.U) && appostrophe == 2)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 0, 5);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ù";
                    afterAppostrophe = true;

    
                }            

                if(Input.GetKeyUp(KeyCode.U) && appostrophe == 4)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 1, 5);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Û";
                    afterAppostrophe = true;

    
                }

                if(Input.GetKeyUp(KeyCode.U) && appostrophe == 3)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 1, 5);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Û";
                    afterAppostrophe = true;

    
                }    

                if(Input.GetKeyUp(KeyCode.U) && appostrophe == 5)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintPortugueseFromTextureIndividual(_PositionX, _PositionY, 2, 5);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ü";
                    afterAppostrophe = true;

    
                }    

            
            }
            


            if(Input.GetKeyUp(KeyCode.A) && appostrophe == 0)
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

        
                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 


                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,0,0);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "A";

            }



            if(Input.GetKeyUp(KeyCode.B))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,1,0);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "B";

            }



            if(Input.GetKeyUp(KeyCode.C))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,2,0);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "C";

            }



            if(Input.GetKeyUp(KeyCode.D))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,3,0);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "D";

            }



            if(Input.GetKeyUp(KeyCode.E) && appostrophe == 0)
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,0,1);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "E";

            }



            if(Input.GetKeyUp(KeyCode.F))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,1,1);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "F";

            }



            if(Input.GetKeyUp(KeyCode.G))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,2,1);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "G";

            }



            if(Input.GetKeyUp(KeyCode.H))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;


                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,3,1);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "H";

            }



            if(Input.GetKeyUp(KeyCode.I) && appostrophe == 0)
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,0,2);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "I";

            }



            if(Input.GetKeyUp(KeyCode.J))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,1,2);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "J";

            }



            if(Input.GetKeyUp(KeyCode.K))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,2,2);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "K";

            }



            if(Input.GetKeyUp(KeyCode.L))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,3,2);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "L";

            }



            if(Input.GetKeyUp(KeyCode.M))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,0,3);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "M";

            }



            if(Input.GetKeyUp(KeyCode.N))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,1,3);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "N";

            }



            if(Input.GetKeyUp(KeyCode.O) && appostrophe == 0)
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,2,3);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "O";

            }



            if(Input.GetKeyUp(KeyCode.P))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,3,3);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "P";

            }



            if(Input.GetKeyUp(KeyCode.Q))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,0,4);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "Q";

            }



            if(Input.GetKeyUp(KeyCode.R))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,1,4);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "R";

            }



            if(Input.GetKeyUp(KeyCode.S))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,2,4);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "S";

            }



            if(Input.GetKeyUp(KeyCode.T))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,3,4);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "T";

            }



            if(Input.GetKeyUp(KeyCode.U))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,0,5);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "U";

            }



            if(Input.GetKeyUp(KeyCode.V))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,1,5);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "V";

            }


            if(Input.GetKeyUp(KeyCode.W))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,2,5);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "W";

            }



            if(Input.GetKeyUp(KeyCode.X))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,3,5);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "X";

            }



            if(Input.GetKeyUp(KeyCode.Y))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,0,6);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "Y";

            }



            if(Input.GetKeyUp(KeyCode.Z))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,1,6);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += "Z";

            }

            if(Input.GetKeyUp(KeyCode.Space))
            {
                int _PositionX = _XaxisTextField;
                int _PositionY = _YaxisTextField;

                paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                PositionPointerX -= 34;
                paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 

                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////        
                paintAlphabetFromTextureIndividual(_PositionX, _PositionY,2,6);
                ///////////////////////////////////
                // Paint letter
                ///////////////////////////////////


                _XaxisTextField -= CHARACTERWIDTH;
                StringTextField += " ";

            }


            ///////////////////////////////////////////////////////////
            // INPUT CHARACTERS OF ITALIAN AND NUMBERS
            ///////////////////////////////////////////////////////////


            if(MethodExtend.keyboardLanguage == 0)
            {

                if(Input.GetKeyUp(KeyCode.Alpha1) && shiftKey == false)
                {
    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY,1,0);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "1";
    
                }
    
                
                if(Input.GetKeyUp(KeyCode.Quote))
                {
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 2, 2);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "À";
    
                }
    
    
    
                if(Input.GetKeyUp(KeyCode.LeftBracket) && shiftKey == false)
                {
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 3, 2);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "È";
    
                }
    
    
                if(Input.GetKeyUp(KeyCode.LeftBracket) && shiftKey == true)
                {
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 0, 3);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "É";
    
                }
    
    
    
                if(Input.GetKeyUp(KeyCode.RightBracket))
                {
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 1, 3);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ì";
                    Debug.Log("Ì");
    
                }
    
    
    
                if(Input.GetKeyUp(KeyCode.Semicolon))
                {
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 2, 3);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ò";
    
                }
    
    
    
                if(Input.GetKeyUp(KeyCode.Backslash))
                {
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 3, 3);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "Ù";
    
                }
    
            
                if(Input.GetKeyUp(KeyCode.Minus) && shiftKey == false )
                {
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 0, 4);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "’";
    
                }
    
    
                if(Input.GetKeyUp(KeyCode.Minus) && shiftKey == true)
                {
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 1, 4);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "‘";
    
                }    

                if(Input.GetKeyUp(KeyCode.Slash) && shiftKey == true)
                {
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 2, 4);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "?";
    
                }    

                if(Input.GetKeyUp(KeyCode.Alpha1) && shiftKey == true)
                {
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 3, 4);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += "!";
    
                }    

                if(Input.GetKeyUp(KeyCode.Comma) && shiftKey == false)
                {
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 0, 5);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += ",";
    
                }


                if(Input.GetKeyUp(KeyCode.Period) && shiftKey == false)
                {
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 1, 5);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += ".";
    
                }



                if(Input.GetKeyUp(KeyCode.Period) && shiftKey == true)
                {
                    
                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 2, 5);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += ":";
    
                }

                if(Input.GetKeyUp(KeyCode.Comma) && shiftKey == true)
                {

                    int _PositionX = _XaxisTextField;
                    int _PositionY = _YaxisTextField;

                    paintPointerPositionBlack(PositionPointerX, PositionPointerY); 
                    PositionPointerX -= 34;
                    paintPointerPosition(PositionPointerX, PositionPointerY,(int) timeNow); 
    
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////        
                    paintNumberFromTextureIndividual(_PositionX, _PositionY, 3, 5);
                    ///////////////////////////////////
                    // Paint letter
                    ///////////////////////////////////
    
    
                    _XaxisTextField -= CHARACTERWIDTH;
                    StringTextField += ";";
    
                }

            }

    }

}

