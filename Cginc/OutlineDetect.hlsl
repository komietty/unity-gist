#ifndef EDGE_DRAW 
#define EDGE_DRAW 

float OutlineDepth(float2 uv){
    float tx = _CameraDepthTexture_TexelSize.x * 1.5f;// * _Thick;
    float ty = _CameraDepthTexture_TexelSize.y * 1.5f;// * _Thick;

    float col00 = Linear01Depth(UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, uv + half2(-tx, -ty))));
    float col10 = Linear01Depth(UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, uv + half2(  0, -ty))));
    float col01 = Linear01Depth(UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, uv + half2(-tx,   0))));
    float col11 = Linear01Depth(UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, uv + half2(  0,   0))));
    float val = (col00 - col11) * (col00 - col11) + (col10 - col01) * (col10 - col01);

    //return (val < _EdgeThreshold) ? 1 : 0;
    return 1 - step(val, 0.000001);
}

float OutlineColor(float2 uv){
    float tx = _MainTex_TexelSize.x * 0.1f;// * _Thick;
    float ty = _MainTex_TexelSize.y * 0.1f;// * _Thick;

    float col00 = Linear01Depth(UNITY_SAMPLE_DEPTH(tex2D(_MainTex, uv + half2(-tx, -ty))));
    float col10 = Linear01Depth(UNITY_SAMPLE_DEPTH(tex2D(_MainTex, uv + half2(  0, -ty))));
    float col01 = Linear01Depth(UNITY_SAMPLE_DEPTH(tex2D(_MainTex, uv + half2(-tx,   0))));
    float col11 = Linear01Depth(UNITY_SAMPLE_DEPTH(tex2D(_MainTex, uv + half2(  0,   0))));
    float val = (col00 - col11) * (col00 - col11) + (col10 - col01) * (col10 - col01);

    //return (val < _EdgeThreshold) ? 1 : 0;
    return 1 - step(val, 0.00003);
}

#endif