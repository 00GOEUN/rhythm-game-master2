블루투스에 보내는 데이터 형식.
==================
   
Center 스크립트의 152줄 주석처리 해제.   
BluetoothLEDLineTrigger 스크립트의 34줄과 53줄 주석처리 해제.   
   
유니티에서 ControlCenter 오브젝트의 인스펙터 창에서 맨 아래   
(LED) 온 오프, (LEDTime) LED가 몇초 전 켜지는지, (LEDDuration) LED가 몇초동안 켜져있는지에 대한 조정 가능   
LED는 체크박스, 다른 둘은 실수형 숫자로 조정.   
LEDTime는 0초, LEDDuration은 0.1초가 최소단위. 그 이하는 자동으로 최소단위로 맞춰지게 코딩 되어있음.   
   
스마트 드럼 LED 프로토콜에 따라 S1OE, S1FE등 LED정보가 string 형태로 블루투스로 수신하게끔 코딩 되어있음.   
만약 LED가 꺼지기 전에 노트가 또 내려온다면 LED를 끄지 않고 LED의 지속시간을 새로운 노트에 맞춰서 갱신함.