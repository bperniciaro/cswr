
/*************************/
/* Cheat Sheet Note Functions */
/*************************/
function EditNote(labNoteID, panViewNoteID, panEditNoteID, tbNoteID, phDeleteID, imaEditID, phSaveID, imaCancelID, hfFullNoteID) {
  // get references to the appropriate controls
  var labNote = document.getElementById(labNoteID);
  var panViewNote = document.getElementById(panViewNoteID);
  var panEditNote = document.getElementById(panEditNoteID);
  var tbNote = document.getElementById(tbNoteID);
  var phDelete = document.getElementById(phDeleteID);
  var imaCancel = document.getElementById(imaCancelID);
  var phSave = document.getElementById(phSaveID);
  var imaEdit = document.getElementById(imaEditID);
  var hfFullNote = document.getElementById(hfFullNoteID);

  // hide the panel that shows the note
  panViewNote.className = 'invisible';
  // hide the 'delete' button
  phDelete.className = "invisible";
  // hide the 'edit' button
  imaEdit.className = "invisible";
  // show the panel that shows the note
  panEditNote.className = 'visible editNoteContainer';
  // show the 'save' button
  phSave.className = "visible";
  // show the 'cancel' button
  imaCancel.className = "visible";

  var decodedNote = htmlDecode(hfFullNote.value);

  // copy the label text to the textbox text
  if (document.all) {  // (document.all refers to ID)
    tbNote.value = decodedNote;
  }
  else {
    tbNote.value = decodedNote;
  }
  // set a focus to the textbox and select everything
  tbNote.focus();
  tbNote.select();
}

function CancelEdit(panEditNoteID, panViewNoteID, imaAddID, labNoteID, phDeleteID, imaEditID, imaCancelID, phSaveID) {
  // get references to the appropriate controls
  var panEditNote = document.getElementById(panEditNoteID);
  var panViewNote = document.getElementById(panViewNoteID);
  var imaAdd = document.getElementById(imaAddID);
  var labNote = document.getElementById(labNoteID);
  var imaEdit = document.getElementById(imaEditID);
  var imaCancel = document.getElementById(imaCancelID);
  var phSave = document.getElementById(phSaveID);
  var phDelete = document.getElementById(phDeleteID);

  // hide the 'edit' panel
  panEditNote.className = 'invisible';

  // show the view panel and note label
  if (document.all) {
    if (labNote.innerText == '') {
      imaAdd.className = 'visible';
    }
    else {
      labNote.className = 'visible';
      panViewNote.className = 'visibleBlock viewNoteContainer';
      phDelete.className = 'visible';
      imaEdit.className = "visible";
    }
  }
  else {
    if (labNote.textContent == '') {
      imaAdd.className = 'visible';
    }
    else {
      labNote.className = 'visible';
      panViewNote.className = 'visibleBlock viewNoteContainer';
      phDelete.className = 'visible';
      imaEdit.className = "visible";
    }
  }

  imaCancel.className = "invisible";
  phSave.className = "invisible";

}

function AddNote(imaAddID, panEditNoteID, tbEditNoteID, imaCancelID, phSaveID) {

  // show/hide the appropriate controls
  $("#" + imaAddID).removeClass().addClass('invisible');
  $("#" + panEditNoteID).removeClass().addClass('editNoteContainer visible');
  $("#" + imaCancelID).removeClass().addClass("visible");
  $("#" + phSaveID).removeClass().addClass("visible");

  // initialize the textbox to empty
  $("#" + tbEditNoteID).val("");
  $("#" + tbEditNoteID).focus();
  //$("#" + tbEditNoteID).select();
}


function DeleteNote(commandArgument, panViewNoteID, imaAddID, labNoteID, panEditNoteID, imaEditID, phSaveID, phDeleteID, tbNoteID) {

  // get references to the appropriate controls
  var panViewNote = document.getElementById(panViewNoteID);
  var imaAdd = document.getElementById(imaAddID);
  var labNote = document.getElementById(labNoteID);
  var panEditNote = document.getElementById(panEditNoteID);
  var imaEdit = document.getElementById(imaEditID);
  var phSave = document.getElementById(phSaveID);
  var phDelete = document.getElementById(phDeleteID);
  var tbNote = document.getElementById(tbNoteID);

  var result = window.confirm("Are you sure you want to delete this note?");
  if (result == true) {

    if (document.all) {
      labNote.innerText = "";
    }
    else {
      labNote.textContent = "";
    }
    tbNote.value = "";
    PageMethods.DeleteNote(commandArgument);
    imaAdd.className = "visible";
    imaEdit.className = "invisible";
    phDelete.className = "invisible";
    panViewNote.className = "invisible";
  }
  else {
    return false;
  }

}

function SaveNote(commandArgument, tbNoteInputID, panNoteEditID, panViewNoteID, labNoteID, imaEditID, phSaveID, imaAddID, imaCancelID, phDeleteID, hfFullNoteID, imaNoteSummaryID, maxNoteLength) {

  var tbNoteInput = document.getElementById(tbNoteInputID);
  var panNoteEdit = document.getElementById(panNoteEditID);
  var panViewNote = document.getElementById(panViewNoteID);
  var labNote = document.getElementById(labNoteID);

  var imaEdit = document.getElementById(imaEditID);
  var imaAdd = document.getElementById(imaAddID);
  var phSave = document.getElementById(phSaveID);
  var imaCancel = document.getElementById(imaCancelID);
  var phDelete = document.getElementById(phDeleteID);

  var hfFullNote = document.getElementById(hfFullNoteID);
  var imaNoteSummary = document.getElementById(imaNoteSummaryID);


  if (tbNoteInput.value == "") {
    panNoteEdit.className = 'invisible';
    phSave.className = 'invisible';
    imaAdd.className = 'visible';
    imaCancel.className = 'invisible';
  }
  else {
    panNoteEdit.className = 'invisible';
    panViewNote.className = 'visibleBlock viewNoteContainer';
    labNote.className = 'visible';
    imaEdit.className = 'visible';
    phSave.className = 'invisible';
    imaCancel.className = 'invisible';
    phDelete.className = 'visible';
  }

  // save the new note in the hidden field for safe keeping
  hfFullNote.value = htmlEncode(tbNoteInput.value);

  // now we need to determine if we need to trim the new string to show it in the label
  var fullNote = tbNoteInput.value;  // strips html tags

  var noteDisplayedAfterSave;

  if (fullNote.length > maxNoteLength) {
    // calculate the shortene Note which we'll display to the user
    noteDisplayedAfterSave = jQuery.trim(fullNote).substring(0, maxNoteLength).split(" ").slice(0, -1).join(" ");
    // show  note summary icon
    imaNoteSummary.className = 'visible';
    imaNoteSummary.title = fullNote;
    //alert("max length: " + maxNoteLength + ", note length: " + fullNote.length);
  }
  else {
    noteDisplayedAfterSave = fullNote;
    // hide note summary
    imaNoteSummary.className = 'invisible';
  }

  /* we have to re-load the qtips after each change so they reflect the latest user action */
  $("#" + imaNoteSummaryID).qtip({
    content: {
      text: imaNoteSummary.title
    }
  }); /* close qtip */


  var encodedNote = htmlEncode(fullNote);
  tbNoteInput.value = encodedNote;

  //alert("saving: " + tbNoteInput.value);
  
  /* Call the server to save the note */
  // if IE
  if (document.all) {
    labNote.innerText = noteDisplayedAfterSave;
    PageMethods.SaveNote(commandArgument, tbNoteInput.value);
  }
  else {
    labNote.textContent = noteDisplayedAfterSave;
    PageMethods.SaveNote(commandArgument, tbNoteInput.value);
  }


}

/* Tag Switching */
function ChangeAndUpdate(elm, commandArg) {
  var state = elm.className != 'tagActive';
  PageMethods.ChangeTag(commandArg, state);
  elm.className = state ? 'tagActive' : 'tagInactive';

  // split the commandArgument so we can determine the tag type
  var pieces = commandArg.split(/[\s-]+/);
  var tagType = pieces[pieces.length - 1];
  switch (tagType) 
  {
    case "sleeper":
      elm.title = state ? 'Click to indicate this player is not a Sleeper.' : 'Click to indicate this player is a Sleeper.';
      break;
    case "bust":
      elm.title = state ? 'Click to indicate this player is not a Bust.' : 'Click to indicate this player is a Bust.';
      break;
    case "injured":
      elm.title = state ? 'Click to indicate this player is not Injured.' : 'Click to indicate this player is Injured.';
      break;
  }

  /* we have to re-load the tag qtips after each change so they reflect the latest user action */
  $("#" + elm.id).qtip({
    content: {
      text: elm.title
    }
  }); /* close qtip */

}


function htmlEncode(value) {
  //create a in-memory div, set it's inner text(which jQuery automatically encodes)
  //then grab the encoded contents back out.  The div never exists on the page.
  return $('<div/>').text(value).html();
}

function htmlDecode(value) {
  return $('<div/>').html(value).text();
}




