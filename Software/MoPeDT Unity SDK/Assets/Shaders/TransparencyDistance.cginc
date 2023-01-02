UNITY_DECLARE_DEPTH_TEXTURE(_DistanceFadeCameraDepthTexture);

fixed transparencyFromObjectDistance(half minDist, half dist1, half dist2, half maxDist) 
{
	half3 viewDirW = _WorldSpaceCameraPos - mul((half4x4)unity_ObjectToWorld, float4(0, 0, 0, 1));
	half viewDist = length(viewDirW);
	
	half falloff;
	if (viewDist < dist1) {
		falloff = 1.0f - saturate((viewDist - minDist) / (dist1 - minDist));
	}
	else if (viewDist > dist2) {
		falloff = saturate((viewDist - dist2) / (maxDist - dist2));
	}
	else {
		falloff = 0;
	}
	
	return 1.0f - falloff;
}

fixed transparencyFromDistanceFrag(half4 position, half minDist, half dist1, half dist2, half maxDist) {
	half viewDist = length(position.xyz - _WorldSpaceCameraPos.xyz);
	
	half falloff;
	if (viewDist < dist1) {
		falloff = 1.0f - saturate((viewDist - minDist) / (dist1 - minDist));
	}
	else if (viewDist > dist2) {
		falloff = saturate((viewDist - dist2) / (maxDist - dist2));
	}
	else {
		falloff = 0;
	}
	
	return 1.0f - falloff;	
}

fixed transparencyFromFragmentDepth(float4 screenPos, half minDist, half dist1, half dist2, half maxDist) {
	float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_DistanceFadeCameraDepthTexture, UNITY_PROJ_COORD(screenPos)));
	float viewDist = abs(sceneZ - screenPos.z);
	
	half falloff;
	if (viewDist < dist1) {
		falloff = 1.0f - saturate((viewDist - minDist) / (dist1 - minDist));
	}
	else if (viewDist > dist2) {
		falloff = saturate((viewDist - dist2) / (maxDist - dist2));
	}
	else {
		falloff = 0;
	}
	
	return 1.0f - falloff;	
}
