/*------------------------------------------------------------------------------
  File:           GitOperationsEditor.cs 
  Project:        YourProjectName  # You should replace this with your project name
  Description:    YourDescription  # You should replace this with your description
  Copyright:      ©2023 YourName/YourCompany. All rights reserved.  # You should replace this with your copyright details

  Last commit by: alchemicalflux 
  Last commit at: 2023-08-22 09:21:25 
------------------------------------------------------------------------------*/
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.GitOperations
{
    /// <summary>
    /// Handles the logic of binding of UI elements and file manipulation for Got operations.
    /// </summary>
    public class GitOperationsEditor
    {
        #region Members

        /// <summary>UI functionality that will be bound to file logic.</summary>
        private GitOperationsEditorUI ui;

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes the UI and binds file manipulation logic.
        /// </summary>
        /// <param name="rootVisualElement"></param>
        public void InitUIComponents(VisualElement rootVisualElement)
        {
            ui = new GitOperationsEditorUI(rootVisualElement);
        }

        #endregion Methods
    }
}