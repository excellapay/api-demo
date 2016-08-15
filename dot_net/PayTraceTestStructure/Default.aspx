

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PayTrace._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="CSS-JS/Core.css" type="text/css" rel="Stylesheet" />
    <link href="CSS-JS/formValidation.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <section class="excellapay">
    <form id="form1" runat="server">
         <div id="pnlForm">

             <p style="padding-left: 180px">
                    <input type="radio" name="frequency" class="input_radio" onclick="paymentMethod_onClick()" value="once"     id="pmOnce"  />One-Time (CC)
                    <input type="radio" name="frequency" class="input_radio" onclick="paymentMethod_onClick()" value="check"    id="pmCheck"    />One-Time (Check)
                    <br />
                    <input type="radio" name="frequency" class="input_radio" onclick="paymentMethod_onClick()" value="weekly"   id="pmWeekly"  />Weekly
                    <input type="radio" name="frequency" class="input_radio" onclick="paymentMethod_onClick()" value="monthly"  id="pmMonthly" />Monthly
                    <input type="radio" name="frequency" class="input_radio" onclick="paymentMethod_onClick()" value="annually" id="pmAnnually"/>Annually
             </p>

            <h3>Billing Information</h3>
            <div class="CustomerBillingInfo">
            
            <div class="form-group">
                <label for="name">Name</label>
                <input name="name" type="text" id="name" class="form-control" maxlength="50" />
            </div>

            <div class="form-group">
                <label for="address1">Address</label>
                <input name="address1" type="text" id="address1" class="form-control" maxlength="50" />
            </div>

            <div class="form-group">
                <label for="address2">Address #2</label>
                <input name="address2" type="text" id="address2" class="form-control" maxlength="50" />
            </div>

            <div class="form-group">
                <label for="city">City</label>
                <input name="city" type="text" id="city" class="form-control" maxlength="50" />
            </div>

            <div class="form-group">
                <label for="state">State</label>
                <input name="state" type="text" id="state" class="form-control" maxlength="50" />
            </div>

            <div class="form-group">
                <label for="zip">Zip Code</label>
                <input name="zip" type="text" id="zip" class="form-control input-small" maxlength="10" />
            </div>

            <div class="form-group">
                <label for="phone">Phone Number</label>
                <input name="phone" type="text" id="phone" class="form-control" maxlength="50" />
            </div>

            <div class="form-group">
                <label for="email">Email Address</label>
                <input name="email" type="text" id="email" class="form-control" maxlength="50" />
            </div>
        </div>
             <h3>Payment Information</h3>
        <div class="divCreditCardInformation">
            <div class="form-group">
                <label for="credit">Credit Card Number</label>
                <input name="credit" type="text" id="credit" class="form-control" maxlength="50" />
            </div>

            <div class="form-group">
                <label for="expMonth">Expiration Date</label>
                <input name="expMonth" type="text" id="x_exp_date_MM" class="form-control input-mini" maxlength="2" placeholder="MM" />
                 &nbsp;/&nbsp;
                <input name="expYear" type="text" id="x_exp_date_YY" class="form-control input-mini" maxlength="4" placeholder="YYYY" />
     &nbsp;</div>

            <div class="form-group">
                <label for="x_card_code">CVV</label>
                <input name="cvv" type="text" id="cvv" class="form-control input-mini" maxlength ="4" />

                    <small class="inline"><a href="#ccv-toggle-window" id="ccv-toggle-link">What is this?</a></small>
                    <div id="ccv-toggle-window" class="toggle-window">
                        <small>The three- or four-digit code assigned to a customer’s credit card number. This number is found either on the back of the card or on the front of the card at the end of the credit card number.</small>
                            <img src="img/ccv.gif" width="296" height="172" alt="security code locations"/>
                        
                    </div>

                

            </div>

            <div class="form-group">
                <label for="amount">Amount</label>
                <div class="input-prepend">
                    <div class="add-on">$</div>
                    <input name="amount" type="text" id="amount" class="form-control input-small" value="1.00" />
                </div>

            </div>

        </div>

        <div class="divCheckInformation">
            <div class="form-group">
                <label for="ddr">Routing Number</label>
                <input name="ddr" type="text" id="ddr" class="form-control" maxlength="20" />
            </div>

            <div class="form-group">
                <label for="tr">Account Number</label>
                <input name="tr" type="text" id="tr" class="form-control input-medium" maxlength="9" />
            </div>
        </div>

        <div class="divRecurInformation">
            <div class="form-group">
                <label for="startMonth">First Date of Payment</label>
                <input name="startMonth" type="text" id="x_start_date_MM" class="form-control input-mini" maxlength="2" placeholder="MM" />
                &nbsp;/&nbsp;
                <input name="startDay" type="text" id="x_start_date_DD" class="form-control input-mini" maxlength="2" placeholder="DD" />
                &nbsp;/&nbsp;
                <input name="startYear" type="text" id="x_start_date_YY" class="form-control input-small" maxlength="4" placeholder="YYYY" />
            </div>

            <div class="form-group">
                <label for="totalCount">Number of Transactions</label>
                <input name="totalCount" type="text" id="totalCount" class="form-control input-mini" maxlength="20" />
            </div>
        </div>

           <div class="tableButtons">
               <input runat="server" type="submit" class="btn btn-primary btn-large" id="btnSubmit" value="Submit" onclick="return onSubmit();" />
               <input style="display: none;" runat="server" type="button" id="btnCancelOrder" value="Cancel Order" onclick="return cancelOrder_onClick();" />
           </div>
        </div>
    </form>
  </section>

    <script type="text/javascript" src="CSS-JS/JQuery.js"></script>
    
    <script>
        $(document).ready(function () {
            // toggle aba message on excellapay
            $('#ccv-toggle-link').click(function () {
                $('#ccv-toggle-window').slideToggle();
            });


            // toggle aba message on excellapay
            $('#aba-toggle-link').click(function () {
                $('#aba-toggle-window').slideToggle();
            });

            var cc = document.getElementsByClassName('divCreditCardInformation');
            var recur = document.getElementsByClassName('divRecurInformation');

            for (var i = 0; i < cc.length; i += 1)
                cc[i].style.display = 'block';

            for (var i = 0; i < recur.length; i += 1)
                recur[i].style.display = 'none';

            document.getElementById('pmOnce').checked = true;

        });
    </script>


    <script>

        var g_submitClicked = false;

        function paymentMethod_onClick() {

            var pmOnce = document.getElementById('pmOnce');
            var pmECheck = document.getElementById('pmCheck');

            var divCC = document.getElementsByClassName('divCreditCardInformation');
            var divCheck = document.getElementsByClassName('divCheckInformation');
            var divRecur = document.getElementsByClassName('divRecurInformation');

            if (pmCheck.checked){
                for (var i = 0; i < divCC.length; i+=1)
                    divCC[i].style.display = 'none';

                for (var i = 0; i < divRecur.length; i += 1)
                    divRecur[i].style.display = 'none';

                for (var i = 0; i < divCheck.length; i += 1)
                    divCheck[i].style.display = 'block';
            } else if (pmOnce.checked){
                for (var i = 0; i < divCC.length; i += 1)
                    divCC[i].style.display = 'block';

                for (var i = 0; i < divRecur.length; i += 1)
                    divRecur[i].style.display = 'none';

                for (var i = 0; i < divCheck.length; i += 1)
                    divCheck[i].style.display = 'none';
            } else {
                for (var i = 0; i < divCC.length; i += 1)
                    divCC[i].style.display = 'block';

                for (var i = 0; i < divRecur.length; i += 1)
                    divRecur[i].style.display = 'block';

                for (var i = 0; i < divCheck.length; i += 1)
                    divCheck[i].style.display = 'none';
            }
        }

        function PopupLink(oLink) {
            if (null != oLink) {
                window.open(oLink.href, null, 'height=350, width=450, scrollbars=1, resizable=1');
                return false;
            }
            return true;
        }

        function ClearHiddenCCEcheck() {
            var oDivCC = document.getElementById('divCreditCardInformation');
            var oDivEcheck = document.getElementById('divBankAccountInformation');
            if (null != oDivCC && null != oDivEcheck) {
                var oFld;
                var list = new Array();
                if ('none' == oDivCC.style.display) {
                    list = new Array('x_card_num', 'x_exp_date_MM', 'x_exp_date_YY', 'x_card_code', 'x_auth_code');
                }
                else if ('none' == oDivEcheck.style.display) {
                    list = new Array('x_bank_name', 'x_bank_acct_num', 'x_bank_aba_code', 'x_bank_acct_name', 'x_drivers_license_num', 'x_drivers_license_state', 'x_drivers_license_dob', 'x_customer_tax_id');
                }
                for (i = 0; i < list.length; i++) {
                    oFld = document.getElementById(list[i]);
                    if (null != oFld && 'text' == oFld.type) oFld.value = '';
                }
            }
        }
        function onSubmit() {
            if (g_submitClicked) {
                return false;
            }
            if (validateFields()) {
                var oBtnSubmit = document.getElementById('btnSubmit');
                oBtnSubmit.value = 'Sending...';
                ClearHiddenCCEcheck();
                return true;
            }
            else {
                return false;
            }



        }
        function MoreLessText(elemId, bMore) {
            if (bMore) {
                document.getElementById(elemId + 'More').style.display = '';
                document.getElementById(elemId + 'Less').style.display = 'none';
            } else {
                document.getElementById(elemId + 'More').style.display = 'none';
                document.getElementById(elemId + 'Less').style.display = '';
            }
            return false;
        }
        function validateFields() {
            document.getElementById("errorMessages").innerHTML = '';

            var addr = document.getElementById('address1');
            var addr2 = document.getElementById('address2');
            var city = document.getElementById('city');
            var state = document.getElementById('state');
            var zip = document.getElementById('zip');
            var cardNum = document.getElementById('credit');
            var expMM = document.getElementById('month');
            var expYY = document.getElementById('year');
            var cvv = document.getElementById('cvv');
            var amnt = document.getElementById('amount');

            if (amnt.value == "") { return focusAndShowRequiredMessage(amnt, 'Amount') }
            if (addr.value == "") { return focusAndShowRequiredMessage(addr, 'Address') }
            if (city.value == "") { return focusAndShowRequiredMessage(city, 'City') }
            if (state.value == "") { return focusAndShowRequiredMessage(state, 'State') }
            if (zip.value == "") { return focusAndShowRequiredMessage(zip, 'Zip') }
            if (cardNum.value == "") { return focusAndShowRequiredMessage(cardNum, 'Card Number') }
            if (expMM.value == "") { return focusAndShowRequiredMessage(expMM, 'Expiration Date Month') }
            if (expYY.value == "") { return focusAndShowRequiredMessage(expYY, 'Expiration Date Year') }
            if (cvv.value == "") { return focusAndShowRequiredMessage(cvv, 'Card Code') }

            /*Minimum amount of $1*/
            if (amnt.value <= 1) { return focusAndShowCustomMessage(amnt, 'The Amount field must be greater than $1.00. Please update the value and try again.') }

            return true;

        }
        function focusAndShowRequiredMessage(focusOnField, friendlyFieldName) {
            var msg = 'The ' + friendlyFieldName + ' field is required. Please enter a value and try again.';
            focusOnField.focus();

            document.getElementById("errorMessages").innerHTML = msg;
            return false;
        }
        function focusAndShowCustomMessage(focusOnField, msg) {
            focusOnField.focus();

            document.getElementById("errorMessages").innerHTML = msg;
            return false;
        }
    </script>


    <script>
        paymentMethod_onClick();
        //document.forms["simForm"].submit();
    </script>
</body>
</html>
