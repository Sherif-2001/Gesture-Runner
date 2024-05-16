# Gesture Runner

Welcome to Gesture Runner! This project is a 2D endless runner game developed using the Unity engine, designed to aid individuals with hand weakness in their rehabilitation process. The game leverages a custom-built magnet glove equipped with Hall effect sensors to control the game, providing an engaging way to help patients regain muscle strength through interactive gameplay.

![Game Screenshot 1](https://github.com/Sherif-2001/Rehabilitation-Project/assets/93449171/46ce6bea-5876-4e6d-b7b5-933dcc9f3cf1)
*Main menu of Gesture Runner*

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Setup and Installation](#setup-and-installation)
- [How to Play](#how-to-play)
- [Calibration](#calibration)
- [Gameplay Mechanics](#gameplay-mechanics)
- [Development](#development)
- [Contributing](#contributing)
- [License](#license)

## Introduction

Gesture Runner is designed to make the process of hand rehabilitation more enjoyable and effective. By using a magnet glove with Hall effect sensors, patients can control the game through specific hand gestures, improving their hand strength and coordination in a fun and motivating environment.

![Game Screenshot 2](https://github.com/Sherif-2001/Rehabilitation-Project/assets/93449171/51704759-bd86-4664-91b9-78ab2462cd53)
*Character running in Gesture Runner*

## Features

- **2D Endless Runner Gameplay**: Navigate your character through an endless world filled with obstacles.
- **Magnet Glove Control**: Use hand gestures detected by Hall effect sensors for game control.
- **Obstacle Avoidance**: Avoid and destroy various obstacles such as fireballs, ice, and cacti.
- **Calibration System**: Easy-to-use calibration process for the glove sensors to ensure accurate control.
- **Engaging Visuals and Sounds**: Attractive graphics and sound effects to enhance the gaming experience.

## Setup and Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/gesture-runner.git
   cd gesture-runner
2. **Unity Setup**:

- **Ensure you have Unity installed on your machine. This project was developed using Unity version** [2.3.14f1].
- **Open the project in Unity.**

## Hardware Setup:
- **Connect the magnet glove with Hall effect sensors to your computer. Make sure all drivers are properly installed**.

![Game Screenshot 3](https://github.com/Sherif-2001/Rehabilitation-Project/assets/93449171/2c5eaa92-4b64-4828-911b-a82c15aad00e)
*Calibration screen for the magnet glove sensors*

## How to Play
1. **Calibration**:
- **At the start of the game, follow the on-screen instructions to calibrate the magnet glove sensors. This step is crucial for accurate gesture detection**.

2. **Start the Game**:
- **Once calibrated, start the game. Your character will begin running automatically.**  

3. **Control the Character**:
- Use specific hand gestures to navigate and avoid or destroy obstacles:
-  Call Gesture: Use this gesture to destroy ice.
-  Gun Gesture: Use this gesture to destroy fireballs.
-  Victory Gesture: Use this gesture to jump over cacti.

![Game Screenshot 4](https://github.com/Sherif-2001/Rehabilitation-Project/assets/93449171/7fe02461-84d8-49d2-a79a-05067d17e733)
Character avoiding obstacles in Gesture Runner

## Calibration
The calibration process ensures that the magnet glove sensors accurately detect hand gestures. Follow these steps for calibration:
1. **Initial Position**: Place your hand in a neutral position.
2. **Follow Prompts**: Move your hand according to the on-screen prompts to calibrate each sensor.
3. **Complete Calibration**: Once all gestures are calibrated, the game will notify you that the calibration is complete.

## Gameplay Mechanics
- Endless Running: The character runs automatically, and the player's goal is to avoid obstacles for as long as possible.
- Obstacle Types:
-   Fireballs: Use the gun gesture to destroy fireballs.
-   Ice: Use the call gesture to destroy ice blocks.
-   Cacti: Use the victory gesture to jump over cacti.

## Development
This project was developed using Unity and C#. The hardware integration involves interfacing the magnet glove with the game via custom scripts that process the sensor data and translate it into game controls.
