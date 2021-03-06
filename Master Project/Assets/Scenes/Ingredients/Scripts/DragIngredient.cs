﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingredients {
    public class DragIngredient : MonoBehaviour
    {

        //distance of the mouse from the center of the spoon
        private Vector3 offset;

        //where mouse is
        private Vector3 mousePosition;

        //where mouse was last frame
        private Vector3 prevMousePosition;

        //how far the spoon traveled
        public float travelDistance = 0;

        public IngredientType ingredientType;

        public Vector3 originalPosition { get; private set; }

        private bool isLockedToLocation = false;

        // Use this for initialization
        void Start()
        {
            originalPosition = this.transform.position;
        }


        /// <summary>
        /// keeps track of distance traveled
        /// </summary>
        void Update()
        {
            if (isLockedToLocation)
            {
                return;
            }

            travelDistance += Vector3.Distance(mousePosition, prevMousePosition);
            prevMousePosition = mousePosition;
        }

        /// <summary>
        /// keeps the spoon for auto-centering on the mouse making it look disconnected from the previous frame
        /// </summary>
        void OnMousePressed()
        {
            if (isLockedToLocation)
            {
                return;
            }

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }

        /// <summary>
        /// moves the spoon
        /// </summary>
        void OnMouseDrag()
        {
            if (isLockedToLocation)
            {
                return;
            }

            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            mousePosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            transform.position = mousePosition;
        }

        /// <summary>
        /// Locks an ingredient to a specific ingredient choice location
        /// </summary>
        /// <param name="choiceLocation">The location to lock an ingredient to</param>
        public void LockToChoiceLocation(IngredientChoiceLocation choiceLocation)
        {
            if (isLockedToLocation)
            {
                return;
            }

            isLockedToLocation = true;
            transform.position = choiceLocation.transform.position;
        }    

        public void SendBackToOriginalPosition()
        {
            transform.position = originalPosition;
        }
    }
}