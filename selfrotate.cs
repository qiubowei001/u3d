using UnityEngine;
using System.Collections;

public class selfrotate : MonoBehaviour {


	Vector3 StartPosition;  //左键按下时鼠标的位置
	Vector3 previousPosition;  //上一帧鼠标的位置。
	Vector3 offset;  //在两帧之间鼠标位置的偏移量，也就是这一帧鼠标的位置减去上一帧鼠标的位置。
	Vector3 finalOffset;  //最终总的鼠标位置的偏移量，即当松开鼠标时那一帧时鼠标的位置减去鼠标按下时的位置。

	bool isSlide;  //鼠标松开后，是否继续旋转一定角度。
	float angle;  //随随便便，大略的就把鼠标位置的偏移量看做是旋转的角度，不是很精确，。，不是重点
	int nMode;//1自动旋转 //0不会自动

	// Use this for initialization
	void Start () {
		//nMode = 1;
		nMode= 0;
	}
	
	public void SetMode (int n) {
		nMode = n;
	}


	void Update()  
	{  


		if (Input.GetMouseButtonDown(0))  
		{  
			StartPosition = Input.mousePosition;  //记录鼠标按下的时候的鼠标位置
			previousPosition = Input.mousePosition;  //记录下当前这一帧的鼠标位置
		}  
		if (Input.GetMouseButton(0))  
		{  
			offset = Input.mousePosition - previousPosition; //这一帧鼠标的位置减去上一帧鼠标的位置就是鼠标的偏移量 
			previousPosition = Input.mousePosition; //再次记录当前鼠标的位置，以备下一帧求offset使用。
			Vector3 Xoffset=new Vector3((offset.x),0,0);//过滤掉鼠标在Y轴方向上的偏移量，只保留X轴的
			transform.Rotate(Vector3.Cross(Xoffset, Vector3.forward).normalized, (offset.magnitude)/4, Space.World);  //旋转



		}  
		if (Input.GetMouseButtonUp(0))  
		{  
			finalOffset = Input.mousePosition - StartPosition;  //鼠标松开时记录最后的鼠标位置
			isSlide = true;  //设为true说明想在鼠标松开后让物体继续旋转一顶焦度
			angle =finalOffset.magnitude; //松开鼠标后需要继续旋转的角度，这个值是随着游戏的update调用一直在递减，否则会一直旋转下去


		}  


		if (isSlide && nMode==1)   //实现松掉鼠标后，还会继续旋转一段距离。如果想在停止后继续旋转的话就执行下面的代码

		{  
			Vector3 XfinalOffset=new Vector3(1,0,0);
			transform.Rotate(Vector3.Cross(XfinalOffset, Vector3.forward).normalized, 5.0f * Time.deltaTime, Space.World);  
			/*
			if (angle > 0)  //下面就是递减控制，防止无限旋转
			{  
				angle -= 5;  
			}  
			else  
			{  
				angle = 0;  
			}  */
		}  
	}  
}


