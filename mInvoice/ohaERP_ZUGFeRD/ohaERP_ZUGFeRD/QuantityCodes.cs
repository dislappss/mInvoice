/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace s2industries.ZUGFeRD
{
    /// <summary>
    /// ISO Quantity Codes
    /// 
    /// for web reference, see e.g.
    /// http://www.robert-kuhlemann.de/iso_masseinheiten.htm
    /// </summary>
    public enum QuantityCodes
    {
        Unknown,
        /// <summary>Kilogramm</summary>
        KGM,    // Kilogramm
        /// <summary>Quadratmeter</summary>
        MTK,    // Quadratmeter   
        /// <summary>Stueck</summary>
        PCE,    // Stück
        /// <summary>Meter</summary>
        MTR,    // Meter     
        /// <summary>Kubikmeter</summary>
        MTQ,
        /// <summary>Tüte</summary>                   
        BG,
        /// <summary>Flasche</summary>                              
        BO,
        /// <summary>Kanister</summary>         
        CA,
        /// <summary>Centiliter</summary> 
        CLT,
        /// <summary>Quadratzentimeter</summary> 
        CMK,
        /// <summary>Kubikcentimeter</summary>       
        CMQ,
        /// <summary>Zentimeter</summary>  
        CMT,//Zentimeter                        
        /// <summary>Kiste</summary>         
        CR, //Kiste                   
        /// <summary>Kasten</summary>  
        CS, //Kasten                  
        /// <summary>Karton</summary>  
        CT, //Karton                  
        /// <summary>Trommel</summary>  
        DR, //Trommel                                                           
        /// <summary>Fuß</summary>   
        FOT,//Fuß                                         
        /// <summary>Gallone</summary>  
        GLL,//US-Gallone              
        /// <summary>Gramm</summary>  
        GRM,//Gramm                             
        /// <summary>Hektar</summary>                  
        HAR,//Hektar                  
        /// <summary>Hektoliter</summary>  
        HLT,//Hektoliter                        
        /// <summary>Stunde</summary>  
        HUR,//Stunde                            
        /// <summary>Zoll</summary>        
        INH,//Zoll                    
        /// <summary>Quadratzoll</summary>  
        INK,//Quadratzoll            
        /// <summary>Kubikzoll</summary>  
        INQ,//Kubikzoll                                    
        /// <summary>Quadrat Kilometer</summary>  
        KMK,//Quadrat Kilometer       
        /// <summary>Kilometer</summary>  
        KMT,//Kilometer                         
        /// <summary>Kilowattstunde</summary>             
        KWH,//Kilowattstunde          
        /// <summary>Kilowatt</summary>  
        KWT,//Kilowatt                          
        /// <summary>US-Pfund</summary>  
        LBR,//US-Pfund                
        /// <summary>Liter</summary>  
        LTR,//Liter                             
        /// <summary>Megawatt</summary>              
        MAW,//Megawatt                          
        /// <summary>Milligramm</summary>  
        MGM,//Milligramm                        
        /// <summary>Minute</summary>  
        MIN,//Minute                            
        /// <summary>Millilitre</summary>        
        MLT,//Millilitre                        
        /// <summary>Quadratmillimeter</summary>       
        MMK,//Quadratmillimeter       
        /// <summary>Kubikmillimeter</summary>  
        MMQ,//Kubikmillimeter         
        /// <summary>Millimeter</summary>  
        MMT,//Millimeter                        
        /// <summary>Monat</summary>        
        MON,//Monat                                                           
        /// <summary>Unze</summary>  
        ONZ,//Unze                    
        /// <summary>Fluid Ounce US</summary>  
        OZA,//Fluid Ounce US                    
        /// <summary>Paket</summary>  
        PA,//Paket                                                      
        /// <summary>Palette</summary>       
        PF, //Palette                 
        /// <summary>Packen</summary>  
        PK, //Packen                  
        /// <summary>Paar</summary>  
        PR, //Paar                    
        /// <summary>Pint, U.S. liquid</summary>  
        PT, //Pint, U.S. liquid                 
        /// <summary>Quart, U.S. liquid</summary>        
        QT, //Quart, U.S. liquid      
        /// <summary>Rolle</summary>  
        RO, //Rolle                             
        /// <summary>Meile</summary>                                       
        SMI,//Meile                   
        /// <summary>US-Tonne</summary>  
        STN,//US-Tonne                
        /// <summary>Tonne (1000 KG)</summary>  
        TNE,//Tonne (1000 KG)                   
        /// <summary>Watt</summary>  
        WTT,//Watt                    
        /// <summary>Quadrat Yard</summary>  
        YDK,//Quadrat Yard            
        /// <summary>Kubik Yard</summary>  
        YDQ,//Kubik Yard              
        /// <summary>Yard</summary>  
        YRD,//Yard                    
    }


    internal static class QuantityCodesExtensions
    {
        public static QuantityCodes FromString(this QuantityCodes _c, string s)
        {
            try
            {
                return (QuantityCodes)Enum.Parse(typeof(QuantityCodes), s);
            }
            catch
            {
                return QuantityCodes.Unknown;
            }
        } // !FromString()


        public static string EnumToString(this QuantityCodes c)
        {
            return c.ToString("g");
        } // !ToString()
    }
}
