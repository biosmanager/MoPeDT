Shader "Unlit/SolidColorDistanceAlpha"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}

        [Toggle(FRAGMENT_BASED_FADE)] 
        _FragmentBasedFade("Fragment based fade", Float) = 0

        _MinVisDistance("MinDistance",Float) = 0
        _Distance1("Distance 1",Float) = 5
        _Distance2("Distance 2",Float) = 15
        _MaxVisDistance("MaxDistance",Float) = 20

        _Color("Color",Color) = (0,0,0,1)
    }
    SubShader
    {
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag 
            // Make fog work
            #pragma multi_compile_fog
            #pragma multi_compile_instancing
            #pragma multi_compile __ FRAGMENT_BASED_FADE

            #include "UnityCG.cginc"
            #include "Vertex2Fragment.cginc"
            #include "TransparencyDistance.cginc"

            sampler2D   _MainTex;
            half        _MinVisDistance;
            half        _Distance1;
            half        _Distance2;
            half        _MaxVisDistance;
            fixed4      _Color;


            v2fw vert(appdata_full v)
            {
                v2fw o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                COMPUTE_EYEDEPTH(o.screenPos.z);
                o.vert = v.vertex;
                o.uv = v.texcoord.xy;
                o.color = _Color;
                return o;
            }

            fixed4 frag(v2fw i) : COLOR
            {
                UNITY_SETUP_INSTANCE_ID(i);
            
#ifdef FRAGMENT_BASED_FADE
                fixed transparency = transparencyFromFragmentDepth(i.screenPos, _MinVisDistance, _Distance1, _Distance2, _MaxVisDistance);
#else
                fixed transparency = transparencyFromObjectDistance(_MinVisDistance, _Distance1, _Distance2, _MaxVisDistance);
#endif
                i.color.a *= transparency;

                fixed4 color = tex2D(_MainTex, i.uv) * i.color;
                return color;
            }
            ENDCG
        }
    }
}
