$("#input_apiKey").change(function() {
    var key = $("#input_apiKey")[0].value;
    if (key && key.trim() != "") {
        console.log(key);
        //swaggerUi.api.clientAuthorizations.add("key", new ApiKeyAuthorization("Authorization", key, "header"));
        $("input[id^=mAuth]").each(function() { $(this).val(key) });


    }
})