package com.jetbrains.rider.plugins.cleancode.options

import com.jetbrains.rider.plugins.cleancode.CleanCodeBundle
import com.jetbrains.rider.settings.simple.SimpleOptionsPage

class CleanCodeOptionsPage : SimpleOptionsPage(
        name= CleanCodeBundle.message("configurable.name.cleancode.options.title"),
        pageId = "CleanCodeAnalysisOptionsPage") {
    override fun getId(): String {
        return "CleanCodeAnalysisOptionsPage";
    }
}