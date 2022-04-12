using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using readDataFromUI;

using readDataFile;

public class PlaneSquare : MonoBehaviour
{

    // Start is called before the first frame update

    private float timeNow = 0;
    private int HEIGHT = 1000;
    private int WIDTH  = 2000;

    private int HEIGHTlargeMemory = 3000;
    private int WIDTHlargeMemory  = 6000;


    private int _Xaxis22size = 1900;
    private int _Yaxis22size = 480;

    public const int CHARACTERWIDTH_size22 = 17;

    Renderer render;
    Material mat;


    public ComputeShader computacion;

    
    ComputeBuffer bufferImageColorPlane;
    
    ComputeBuffer bufferLargeMemory;

    ComputeBuffer bufferTextureColorPositionX;
    ComputeBuffer bufferTextureColorPositionY;

    ComputeBuffer bufferTextureBlanckPositionX;
    ComputeBuffer bufferTextureBlanckPositionY;


    ComputeBuffer bufferTextureCharacterPositionX_size22;
    ComputeBuffer bufferTextureCharacterPositionY_size22;
    
    ComputeBuffer bufferTextureAlphabetColor_size22;
    ComputeBuffer bufferTextureNumberColor_size22;
    ComputeBuffer bufferTexturePortugueseColor_size22;


    ComputeBuffer bufferHorizontalPositionX;
    ComputeBuffer bufferHorizontalPositionY;

    ComputeBuffer bufferVerticalPositionX;
    ComputeBuffer bufferVerticalPositionY;



    static int _kernelforLoadImageColorPlane;

    static int _kernelforLoadTextureColorPosition;
    static int _kernelforPrintTextureColor;
    static int _kernelforPrintTextureEraserColor;


    static int _kernelforLoadTextureBlanckPosition;
    static int _kernelforPrintTextureBlanck;
    
    static int _kernelforLoadPointerPosition;

    static int _kernelforPrintPointer;

    static int _kernelforPrintPointerBlack;
    
    static int _kernelforLoadTextureCharacterPosition_size22;

    static int _kernelforPrintTilesAlphabet_size22;

    static int _kernelforPrintTilesAlphabetIndividual_size22;
    static int _kernelforPrintTilesNumberIndividual_size22;
    static int _kernelforPrintTilesPortugueseIndividual_size22;

    static int _kernelforLoadHorizontalPosition;
    static int _kernelforPrintHorizontal;
    static int _kernelforPrintHorizontalBlack;


    static int _kernelforLoadVerticalPosition;
    static int _kernelforPrintVertical;
    static int _kernelforPrintVerticalBlack;



    bool activo2 = false;
    

    public GameObject cursor;

    public ParticleSystem particle_RedStar0;
    public ParticleSystem particle_RedStar1;


    public ParticleSystem particle_RIGHT0;
    public ParticleSystem particle_RIGHT1;
    public ParticleSystem particle_RIGHT2;
    public ParticleSystem particle_RIGHT3;
    public ParticleSystem particle_RIGHT4;


////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// start of minigame
////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Start()
    {

        render = GetComponent<Renderer>();
        mat = render.material;

        computacion.SetInt("_WIDTH", WIDTH);
        computacion.SetInt("_WIDTHlargeMemory", WIDTHlargeMemory);

        mat.SetInt("_WIDTH", WIDTH);
        mat.SetInt("_HEIGHT", HEIGHT);

        bufferLargeMemory = new ComputeBuffer(HEIGHTlargeMemory * WIDTHlargeMemory, 16);
        bufferImageColorPlane = new ComputeBuffer(HEIGHT * WIDTH, 16);

        mat.SetBuffer("bufferImageColorPlane", bufferImageColorPlane);


        _kernelforLoadImageColorPlane = computacion.FindKernel("forLoadImageColorPlane");

        _kernelforLoadTextureColorPosition = computacion.FindKernel("forLoadTextureColorPosition");
        _kernelforPrintTextureColor = computacion.FindKernel("forPrintTextureColor");
        _kernelforPrintTextureEraserColor = computacion.FindKernel("forPrintTextureEraserColor");

        _kernelforLoadTextureBlanckPosition = computacion.FindKernel("forLoadTextureBlanckPosition");
        _kernelforPrintTextureBlanck = computacion.FindKernel("forPrintTextureBlanck");


        _kernelforLoadTextureCharacterPosition_size22  = computacion.FindKernel("forLoadTextureCharacterPosition_size22");

        _kernelforPrintTilesAlphabet_size22 = computacion.FindKernel("forPrintTilesAlphabet_size22");

        _kernelforPrintTilesAlphabetIndividual_size22 = computacion.FindKernel("forPrintTilesAlphabetIndividual_size22");
        _kernelforPrintTilesNumberIndividual_size22 = computacion.FindKernel("forPrintTilesNumberIndividual_size22");
        _kernelforPrintTilesPortugueseIndividual_size22 = computacion.FindKernel("forPrintTilesPortugueseIndividual_size22");


        _kernelforLoadHorizontalPosition = computacion.FindKernel("forLoadHorizontalPosition");
        _kernelforPrintHorizontal = computacion.FindKernel("forPrintHorizontal");
        _kernelforPrintHorizontalBlack = computacion.FindKernel("forPrintHorizontalBlack");

        _kernelforLoadVerticalPosition = computacion.FindKernel("forLoadVerticalPosition");
        _kernelforPrintVertical = computacion.FindKernel("forPrintVertical");
        _kernelforPrintVerticalBlack = computacion.FindKernel("forPrintVerticalBlack");



        bufferTextureColorPositionX = new ComputeBuffer(136, 4);
        bufferTextureColorPositionY = new ComputeBuffer(136, 4);

        bufferTextureBlanckPositionX = new ComputeBuffer(10000, 4);
        bufferTextureBlanckPositionY = new ComputeBuffer(10000, 4);


        bufferTextureCharacterPositionX_size22 = new ComputeBuffer(16184, 4);
        bufferTextureCharacterPositionY_size22 = new ComputeBuffer(16184, 4);

        bufferTextureAlphabetColor_size22 = new ComputeBuffer(64736, 4); 
        
        bufferTextureNumberColor_size22 = new ComputeBuffer(64736, 4); 

        bufferTexturePortugueseColor_size22 = new ComputeBuffer(64736, 4); 

        
        bufferHorizontalPositionX = new ComputeBuffer(280, 4);
        bufferHorizontalPositionY = new ComputeBuffer(280, 4);

        bufferVerticalPositionX = new ComputeBuffer(280, 4);
        bufferVerticalPositionY = new ComputeBuffer(280, 4);



        computacion.SetBuffer(_kernelforLoadImageColorPlane, "bufferImageColorPlane", bufferImageColorPlane);
        computacion.SetBuffer(_kernelforLoadImageColorPlane, "bufferLargeMemory", bufferLargeMemory);

        computacion.SetBuffer(_kernelforLoadTextureColorPosition, "bufferTextureColorPositionX", bufferTextureColorPositionX);
        computacion.SetBuffer(_kernelforLoadTextureColorPosition, "bufferTextureColorPositionY", bufferTextureColorPositionY);

        computacion.SetBuffer(_kernelforPrintTextureColor,"bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTextureColor,"bufferTextureColorPositionX", bufferTextureColorPositionX);
        computacion.SetBuffer(_kernelforPrintTextureColor,"bufferTextureColorPositionY", bufferTextureColorPositionY);

        computacion.SetBuffer(_kernelforPrintTextureEraserColor,"bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTextureEraserColor,"bufferTextureColorPositionX", bufferTextureColorPositionX);
        computacion.SetBuffer(_kernelforPrintTextureEraserColor,"bufferTextureColorPositionY", bufferTextureColorPositionY);




        computacion.SetBuffer(_kernelforLoadTextureBlanckPosition, "bufferTextureBlanckPositionX", bufferTextureBlanckPositionX);
        computacion.SetBuffer(_kernelforLoadTextureBlanckPosition, "bufferTextureBlanckPositionY", bufferTextureBlanckPositionY);

        computacion.SetBuffer(_kernelforPrintTextureBlanck,"bufferLargeMemory", bufferLargeMemory);
        computacion.SetBuffer(_kernelforPrintTextureBlanck,"bufferTextureBlanckPositionX", bufferTextureBlanckPositionX);
        computacion.SetBuffer(_kernelforPrintTextureBlanck,"bufferTextureBlanckPositionY", bufferTextureBlanckPositionY);




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



        
        loadTextureColorPosition();

        
    }




    // Update is called once per frame
    void Update()
    {


        timeNow = Time.realtimeSinceStartup;


        if(MethodExtend.readFromButton == 1 && activo2 == false)
        {

            loadBufferTexturePosition_size22();

            loadBufferTextureAlphabet_size22();
            
            loadBufferTextureNumber_size22();
            
            loadBufferTexturePortuguese_size22();


            loadBufferHorizontalPosition();

            loadBufferVerticalPosition();


            activo2 = !activo2;

        }




        if(Input.GetKeyUp(KeyCode.F10))
        {
            
            loadTextureBlanckPosition();
            printTextureBlanck();

            Debug.Log("Here in space");
        }




        if(Input.GetKeyUp(KeyCode.F8))
        {

            loadTextureColorPosition();
            
            printTextureColor(1900,130);

        }



        if(Input.GetKeyUp(KeyCode.F8))
        {
        
            Vector3 variableLearn = cursor.transform.position;

            cursor.transform.position = new Vector3(variableLearn.x + 0.1f,variableLearn.y, variableLearn.z);

        }


        if(Input.GetKeyUp(KeyCode.F5))
        {
            
            particle_RedStar0.Play(true);
            particle_RedStar1.Play(true);

            ScriptCommunication.activatePrefabRedStar = false;

        }


        if(ScriptCommunication.activatePrefabRedStar == true)
        {
            
            particle_RedStar0.Play(true);
            particle_RedStar1.Play(true);

            ScriptCommunication.activatePrefabRedStar = false;

        }





    //  upperBoundY = 80 + position_RigthWordList * 150;
    //  lowerBoundY = 80 + 79 + position_RigthWordList * 150 + 7;
    //  leftBoundX = 800;

        loadImageColorPlane();

    }


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//   DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY DESTROY   //
/////////////////////////////////////////////////////////////////////////////////////////////////////////



    void OnDestroy()
    {

        bufferImageColorPlane.Release();
        bufferImageColorPlane.Dispose();
        bufferImageColorPlane = null;

        bufferLargeMemory.Release();
        bufferLargeMemory.Dispose();
        bufferLargeMemory = null;

        bufferTextureColorPositionX.Release();
        bufferTextureColorPositionX.Dispose();
        bufferTextureColorPositionX = null;

        bufferTextureColorPositionY.Release();
        bufferTextureColorPositionY.Dispose();
        bufferTextureColorPositionY = null;

        bufferTextureBlanckPositionX.Release();
        bufferTextureBlanckPositionX.Dispose();
        bufferTextureBlanckPositionX = null;

        bufferTextureBlanckPositionY.Release();
        bufferTextureBlanckPositionY.Dispose();
        bufferTextureBlanckPositionY = null;

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


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// BASE METHODS FOR PLANE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    public void loadImageColorPlane()
    {
     
        computacion.Dispatch(_kernelforLoadImageColorPlane, 2000, 1000, 1);
    
    }    


    public void loadTextureColorPosition()
    {

        computacion.Dispatch(_kernelforLoadTextureColorPosition, 8, 1, 1);

    }


    public void printTextureColor(int Xaxis, int Yaxis)
    {

        int _PositionTextureColorX = Xaxis;
        int _PositionTextureColorY = Yaxis;

        computacion.SetInt( "_PositionTextureColorX", _PositionTextureColorX);
        computacion.SetInt( "_PositionTextureColorY", _PositionTextureColorY);

        computacion.Dispatch(_kernelforPrintTextureColor, 8, 1, 1);

    }


    public void printTextureEraserColor(int Xaxis, int Yaxis)
    {

        int _PositionTextureEraserColorX = Xaxis;
        int _PositionTextureEraserColorY = Yaxis;

        computacion.SetInt( "_PositionTextureEraserColorX", _PositionTextureEraserColorX);
        computacion.SetInt( "_PositionTextureEraserColorY", _PositionTextureEraserColorY);
        
        computacion.Dispatch(_kernelforPrintTextureEraserColor, 8, 1, 1);

    }


    public void loadTextureBlanckPosition()
    {

        computacion.Dispatch(_kernelforLoadTextureBlanckPosition, 100, 1, 1);

    }


    public void printTextureBlanck()
    {


        int _PositionTextureBlanckX = 100;
        int _PositionTextureBlanckY = 100;

        computacion.SetInt( "_PositionTextureBlanckX", _PositionTextureBlanckX);
        computacion.SetInt( "_PositionTextureBlanckY", _PositionTextureBlanckY);

        computacion.Dispatch(_kernelforPrintTextureBlanck, 100, 1, 1);

    }


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// BASE METHODS FOR PLANE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////



///////////////////////////////////////////////////////////////////////////////////////////////////////////
// PAINT BOUNDS
///////////////////////////////////////////////////////////////////////////////////////////////////////////


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


///////////////////////////////////////////////////////////////////////////////////////////////////////////
// PAINT BOUNDS
///////////////////////////////////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////////////////////////////////////////
// LOAD AND PAINT WORD SIZE 22
///////////////////////////////////////////////////////////////////////////////////////////////////////////




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


///////////////////////////////////////////////////////////////////////////////////////////////////////////
// LOAD AND PAINT WORD SIZE 22
///////////////////////////////////////////////////////////////////////////////////////////////////////////



}
        