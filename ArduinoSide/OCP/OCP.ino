#include "MUX74HC4067.h"
#include "GyverButton.h"

int oldPVals[16], curPVal, oldBVals[8];

MUX74HC4067 mux(7, 8, 9, 10, 11);
GButton butt0(2);

void setup()
{
  Serial.begin(9600);
  while (!Serial) ;
  
  mux.signalPin(A0, INPUT, ANALOG);
}

void loop()
{
  butt0.tick();
  if(butt0.isClick()){
    Serial.println("20:0");
  }
  if(butt0.isSingle()){
    Serial.println("20:1");
  }
  if(butt0.isDouble()){
    Serial.println("20:2");
  }
  if(butt0.isTriple()){
    Serial.println("20:3");
  }
  if(butt0.isHolded()){
    Serial.println("20:4");
  }
//  for (byte i = 0; i < 16; ++i)
//  {
//    curPVal = round((double)(mux.read(i)) * 100 / 1023);
//    if (oldPVals[i] != curPVal){
//      if (i < 8){
//        Serial.print("0");
//      }else{
//        Serial.print("1");
//      }
//      Serial.print(i);
//      Serial.print(":");
//      Serial.println(curPVal);
//      
//      oldPVals[i] = curPVal;
//    }
//    delay(10);
//  }  
}
