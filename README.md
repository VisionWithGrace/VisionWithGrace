VisionWithGrace
===============

## Description

VisionWithGrace is a Windows application designed to help people with Cerebral Palsy in communicating their needs.
The software uses a Microsoft Kinect to view and recognize objects in its field of view.
Objects the Kinect recognizes will be highlighted in the GUI and holding down the spacebar will cycle through the highlighted objects.

## Prerequisites

* Kinect for Windows sensor
* Windows 7+
* Visual Studio 2012

## Installation

1. Install the [Kinect for Windows SDK](http://www.microsoft.com/en-us/kinectforwindowsdev/Start.aspx)
2. Clone VisionWithGrace: `$ git clone https://github.com/VisionWithGrace/VisionWithGrace.git`
3. Download the following binaries using the following links. These were too big for GitHub:
	* http://tklovett.com/assets/opencv_gpufilters290.dll
	* http://tklovett.com/assets/nppc32_55.dll
4. Paste BOTH of these files into BOTH of the following locations:
	* VisionWithGrace\VisionWithGrace\bin\Debug
	* VisionWithGrace\VisionWithGrace\bin\Release

## Start up

1. Plug in the Kinect to power and your USB port. Wait for any drivers to install and for the Kinect to initialize
2. Open VisionWithGrace/VisionWithGrace.sln with Visual Studio 2012
3. Click Start Debugging (F5)

## Usage

1. Press down on the space bar to scan through the objects
2. Release the space bar to select an object
3. Press space bar to dismiss the Selected Object dialog or fill in information for and save the object
