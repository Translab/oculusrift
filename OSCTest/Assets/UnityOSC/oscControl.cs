//
//	  UnityOSC - Example of usage for OSC receiver
//
//	  Copyright (c) 2012 Jorge Garcia Martin
//
// 	  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// 	  documentation files (the "Software"), to deal in the Software without restriction, including without limitation
// 	  the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
// 	  and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// 	  The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// 	  of the Software.
//
// 	  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// 	  TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// 	  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// 	  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// 	  IN THE SOFTWARE.
//

using UnityEngine;

using System;

using System.Collections;

using System.Collections.Generic;

using System.Text;

using UnityOSC;



public class oscControl : MonoBehaviour {

	

	private Dictionary<string, ServerLog> servers;	
	
	public GameObject camera; // Main Camera GameObject
	
	public GameObject leftHand; // left hand object
	
	Vector3 pos, posLeftHand;
	

	// Script initialization

	void Start() {	

		OSCHandler.Instance.Init(); //init OSC

		servers = new Dictionary<string, ServerLog>();		
		
		//Debug.Log(player.transform.position.ToString());

	}



	// NOTE: The received messages at each server are updated here

    // Hence, this update depends on your application architecture

    // How many frames per second or Update() calls per frame?

	void Update() {
		

		OSCHandler.Instance.UpdateLogs(); // needed to read the incoming messages from servers

		servers = OSCHandler.Instance.Servers;
		
		//pos = Vector3.zero;
		

		// for each server, do stuff

	    foreach( KeyValuePair<string, ServerLog> item in servers )

		{

			// If we have received at least one packet,

			// show the last received from the log in the Debug console

			if(item.Value.log.Count > 0) 

			{

				int lastPacketIndex = item.Value.packets.Count - 1;
				
				//string serverName = item.Key;
				string oscAddress = item.Value.packets[lastPacketIndex].Address;
				//string dataValue;// = item.Value.packets[lastPacketIndex].Data[0].ToString();
				
				

				

				//UnityEngine.Debug.Log(String.Format("SERVER: {0} ADDRESS: {1} VALUE 0: {2}", 

				//                                    item.Key, // Server name

				//                                    item.Value.packets[lastPacketIndex].Address, // OSC address

				//                                    item.Value.packets[lastPacketIndex].Data[0].ToString())); //First data value
				
				if(oscAddress == "/rift/xyz/") {
					string xVal = item.Value.packets[lastPacketIndex].Data[0].ToString();
					string yVal = item.Value.packets[lastPacketIndex].Data[1].ToString();
					string zVal = item.Value.packets[lastPacketIndex].Data[2].ToString();
					//print(dataValue);
					
					pos.x = float.Parse(xVal) * 1;
					pos.y = float.Parse(yVal) * 1;
					pos.z = float.Parse(zVal) * 1;
				
					//float z = float.Parse(zVal);
					//float x = float.Parse(xVal);
					//print(z);
										
					camera.transform.position = new Vector3(pos.x,pos.y,pos.z);
				}
				
				if(oscAddress == "/lefthand/xyz/") {
					string xVal = item.Value.packets[lastPacketIndex].Data[0].ToString();
					string yVal = item.Value.packets[lastPacketIndex].Data[1].ToString();
					string zVal = item.Value.packets[lastPacketIndex].Data[2].ToString();
					//print(dataValue);
					
					posLeftHand.x = float.Parse(xVal) * 1;
					posLeftHand.y = float.Parse(yVal) * 1;
					posLeftHand.z = float.Parse(zVal) * 1;
				
					//float z = float.Parse(zVal);
					//float x = float.Parse(xVal);
					//print(z);
										
					leftHand.transform.position = new Vector3(posLeftHand.x,posLeftHand.y,posLeftHand.z);
				}
							
			}

	    }

	}

}